using wServer.realm;
using wServer.realm.entities;
using wServer.networking.packets.outgoing;
using common.resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace wServer.logic.behaviors


{
    internal class PetProjectile : Behavior
    {

        Player player;
        public PetProjectile(int damage, int range, int coolDown, int angle, uint projId, ConditionEffectIndex effect = ConditionEffectIndex.Speedy, int effDuration = 3)
        {
          
        }
        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = 0;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {          
        }
    }
}