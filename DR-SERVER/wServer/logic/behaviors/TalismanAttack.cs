using wServer.realm;
using wServer.realm.entities;
using wServer.networking.packets.outgoing;
using common.resources;
namespace wServer.logic.behaviors
{
    internal class TalismanAttack : Behavior
    {
        private readonly int _range;
        private readonly uint _color;
        private readonly int _coolDown;
        private readonly int _damage;
        private readonly int _duration;
        ConditionEffectIndex _effect;

        public TalismanAttack(int damage, ConditionEffectIndex effect, int range=6, int duration = 0, int coolDown = 0, uint color = 0)
        {
            _range = range;
            _damage = damage;
            _effect = effect;
            _duration = duration;
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
                var entities = host.GetNearestEntities(_range, 1);//1 for enemy

                Enemy en = null;
                foreach (Entity e in entities)
                    if (e is Enemy)
                    {
                        en = e as Enemy;
                        break;
                    }

                if (en != null)
                {
                    en.Owner.BroadcastPacket(new ShowEffect()
                    {
                        EffectType = EffectType.AreaBlast,
                        Color = new ARGB(_color),
                        TargetObjectId = en.Id,
                        Pos1 = new Position { X = 1, }
                    }, null);
                    en.Owner.BroadcastPacket(new ShowEffect()
                    {
                        EffectType = EffectType.Trail,
                        TargetObjectId = host.Id,
                        Pos1 = new Position { X = en.X, Y = en.Y },
                        Color = new ARGB(_color)
                    }, null);
                    en.Damage(host.GetPlayerOwner(), time, _damage, true);
                    en.ApplyConditionEffect(new ConditionEffect
                    {
                        Effect = _effect,
                        DurationMS = _duration
                    });
                }
                cool = _coolDown; 
            }
            else
                cool -= time.ElaspedMsDelta;

            state = cool;
        }
    }
}