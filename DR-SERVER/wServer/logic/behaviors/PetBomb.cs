using wServer.realm;
using wServer.realm.entities;
using wServer.networking.packets.outgoing;
using common.resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace wServer.logic.behaviors


{
    internal class PetBomb : Behavior
    {
        private readonly int _radius;
        private readonly uint _color;
        private readonly int _coolDown;
        private readonly int _damage;
        private readonly int _effDuration;
        ConditionEffectIndex _effect;

        Player player;
        public PetBomb(int damage, int radius = 3, ConditionEffectIndex effect = ConditionEffectIndex.Speedy, int effDuration = 0, int coolDown = 0, uint color = 0)
        {
            _effect = effect;//add condeffect nothing
            _damage = damage;
            _radius = radius;
            _effDuration = effDuration;
            _coolDown = coolDown;
            _color = color == 0 ? 0xFF0000 : color;
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
                var entities = host.GetNearestEntities(_radius, 0);//1 for enemy

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
                //AOE Attack
                host.Owner.BroadcastPacket(new ShowEffect()
                {
                    EffectType = EffectType.AreaBlast,
                    Color = new ARGB(_color),
                    TargetObjectId = host.Id,
                    Pos1 = new Position { X = _radius, }
                }, null);
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

                cool = _coolDown;
            }
            else
                cool -= time.ElaspedMsDelta;

            state = cool;
        }
    }
}