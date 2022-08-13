using common.resources;
using System.Collections.Generic;
using System.Linq;
using wServer.realm.entities.player.equipstatus;

namespace wServer.realm.entities
{
    public partial class Player
    {
        private static readonly Dictionary<EquippedStatus, EquippedStatus[]> _sets = new Dictionary<EquippedStatus, EquippedStatus[]>()
        {

        };

        public Dictionary<EquippedStatus, IEquipStatus> EquipStatus { get; private set; }

        protected void OnEquippedChanged()
        {
            if (EquipStatus == null)
                EquipStatus = new Dictionary<EquippedStatus, IEquipStatus>();

            var status = new List<EquippedStatus>();
            for (var i = 0; i < 4; i++)
                if (Inventory[i] != null)
                    foreach (var s in Inventory[i].EquipmentStatus)
                        if (!status.Contains(s))
                            status.Add(s);

            //handle sets
            foreach (var set in _sets)
                if (set.Value.All(_ => status.Contains(_)))
                {
                    foreach (var s in set.Value)
                        status.Remove(s);
                    status.Add(set.Key);
                }

            foreach (var s in EquipStatus.ToArray())
            {
                if (status.Contains(s.Key))
                {

                    status.Remove(s.Key);
                    continue;
                }
                if (Admin)
                {
                    //SendInfo("unequip + " + s.Key.ToString());
                }
                s.Value.Unequip(this);
                EquipStatus.Remove(s.Key);
            }

            foreach (var s in status)
            {
                if (!Manager.EquippedManager.Contians(s))
                {
                    if (Admin)
                    {
                        // SendError("Unimplemented equippedeffect: " + s.ToString());
                    }
                    continue;
                }

                var instance = Manager.EquippedManager.CreateInstance(s);
                instance.OnEquip(this);
                EquipStatus.Add(s, instance);
            }
        }

        protected void EquippedStatusTick(RealmTime time)
        {
            foreach (var status in EquipStatus.Values)
                status.OnTick(this, time);
        }

        protected void EquippedStatusHit(int dmg)
        {
            foreach (var status in EquipStatus.Values)
                status.OnHit(this, dmg);
        }
    }
}
