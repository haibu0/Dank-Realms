﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using common.resources;
using wServer.logic;
using wServer.networking.packets.outgoing;

namespace wServer.realm.entities
{
    class Trap : StaticObject
    {
        const int LIFETIME = 10;

        Player player;
        float radius;
        int dmg;
        ConditionEffectIndex effect;
        int duration;
        uint color;
        public Trap(Player player, float radius, int dmg, ConditionEffectIndex eff, float effDuration, uint color = 0xff9000ff)
            : base(player.Manager, 0x0711, LIFETIME * 1000, true, true, false)
        {
            this.player = player;
            this.radius = radius;
            this.dmg = dmg;
            this.effect = eff;
            this.duration = (int)(effDuration * 1000);
            this.color = color;
        }

        int t = 0;
        int p = 0;
        public override void Tick(RealmTime time)
        {
            if (t / 500 == p)
            {
                Owner.BroadcastPacketNearby(new ShowEffect()
                {
                    EffectType = EffectType.Trap,
                    Color = new ARGB(color),
                    TargetObjectId = Id,
                    Pos1 = new Position() { X = radius / 2 }
                }, this, null);
                p++;
                if (p == LIFETIME * 2)
                {
                    Explode(time);
                    return;
                }
            }
            t += time.ElaspedMsDelta;

            bool monsterNearby = false;
            this.AOE(radius / 2, false, enemy => monsterNearby = true);
            if (monsterNearby)
                Explode(time);

            base.Tick(time);
        }

        void Explode(RealmTime time)
        {
            Owner.BroadcastPacketNearby(new ShowEffect()
            {
                EffectType = EffectType.AreaBlast,
                Color = new ARGB(color),
                TargetObjectId = Id,
                Pos1 = new Position() { X = radius }
            }, this, null);

          
            var enemies = new List<Enemy>();
            
            

            this.AOE(radius, false, enemy => enemies.Add(enemy as Enemy));
            {
                if (enemies.Count() > 0)
                foreach (var enemy in enemies)
                enemy?.Damage(player, time, dmg, false, new ConditionEffect()
                {
                    Effect = effect,
                    DurationMS = duration
                });
            };
            Owner.LeaveWorld(this);
        }
    }
}
