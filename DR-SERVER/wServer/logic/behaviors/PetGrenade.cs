using wServer.realm;
using wServer.realm.entities;
using wServer.networking.packets.outgoing;
using common.resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace wServer.logic.behaviors


{
    internal class PetGrenade : Behavior
    {
        private readonly int _radius;
        private readonly uint _color;
        private readonly int _coolDown;
        private readonly int _damage;
        private readonly int _effDuration;
        ConditionEffectIndex _effect;
        private readonly double _range;
        private readonly double? _fixedAngle;
        public PetGrenade(int damage, int radius, int coolDown, double? fixedAngle = null, double range = 0, ConditionEffectIndex effect = ConditionEffectIndex.Speedy, int effDuration = 0, uint color = 0)
        {
            _effect = effect;//add condeffect nothing
            _damage = damage;
            _radius = radius;
            _effDuration = effDuration;
            _coolDown = coolDown;
            _color = color == 0 ? 0xFF0000 : color;
            _range = range;
            _fixedAngle = fixedAngle * Math.PI / 180;
        }
        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = 0;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            int cool = (int)state;
            if (cool <= 0)
            {
                var entities = host.GetNearestEntities(_radius, 1);//1 for enemy

                Enemy en = null;
                foreach (Entity e in entities)
                    if (e is Enemy)
                    {
                        en = e as Enemy;
                        break;
                    }

                var target = new Position()
                {
                    X = host.X,
                    Y = host.Y,
                };
                
                if (_fixedAngle != null)
                    target = new Position()
                    {
                        X = (float)(_range * Math.Cos(_fixedAngle.Value)) + host.X,
                        Y = (float)(_range * Math.Sin(_fixedAngle.Value)) + host.Y,
                    };
                
                host.Owner.BroadcastPacketNearby(new ShowEffect()
                {
                    EffectType = EffectType.Throw,
                    Color = new ARGB(_color),
                    TargetObjectId = host.Id,
                    Pos1 = target,
                    AirTime = _coolDown
                }, host, null);
                host.Owner.Timers.Add(new WorldTimer(_coolDown, (world, t) =>
                {
                    /*
                    host.Owner.BroadcastPacketNearby(new ShowEffect()
                    {
                        EffectType = EffectType.Diffuse,
                        Color = new ARGB(_color),
                        TargetObjectId =host.Id,
                        Pos1 = target,
                        Pos2 = new Position { X = target.X + _radius, Y = target.Y }
                    }, host, null);
                    
                    var enemies = new List<Enemy>();
                    host.AOE(_radius, false, enemy => enemies.Add(enemy as Enemy));
                    {
                        if (enemies.Count() > 0)
                            foreach (var enemy in enemies)
                                enemy?.Damage(host.GetPlayerOwner(), time, _damage, false, new ConditionEffect()
                                {
                                    Effect = _effect,
                                    DurationMS = _effDuration
                                });
                    };
                    */
                }));
                cool = _coolDown;
            }
            else
                cool -= time.ElaspedMsDelta;

            state = cool;
        }
    }
}