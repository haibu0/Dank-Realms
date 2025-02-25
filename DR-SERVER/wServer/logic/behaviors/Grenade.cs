﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using common.resources;
using wServer.networking.packets.outgoing;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.behaviors
{
    class Grenade : Behavior
    {
        //State storage: cooldown timer
        bool isBomb;
        double range;
        float radius;
        double? fixedAngle;
        int damage;
        Cooldown coolDown;
        ConditionEffectIndex effect;
        int effectDuration;
        new uint color;

        public Grenade(double radius = 0, int damage = 0, double range = 5,
            double? fixedAngle = null, Cooldown coolDown = new Cooldown(), ConditionEffectIndex effect = 0, int effectDuration = 0, uint color = 0xffff0000, bool isBomb = false)
        {
            this.isBomb = isBomb;
            this.radius = (float)radius;
            this.damage = damage;
            this.range = range;
            this.fixedAngle = fixedAngle * Math.PI / 180;
            this.coolDown = coolDown.Normalize();
            this.effect = effect;
            this.effectDuration = effectDuration;
            this.color = color;
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = 0;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            int cool = (int)state;
            var timer = isBomb ? 0 : 1500;

            if (cool <= 0)
            {
                if (host.HasConditionEffect(ConditionEffects.Stunned))
                    return;

                var player = host.AttackTarget ?? host.GetNearestEntity(range, true);
                if (player != null || fixedAngle != null)
                {
                    Position target;
                    if (fixedAngle != null)
                        target = new Position()
                        {
                            X = (float)(range * Math.Cos(fixedAngle.Value)) + host.X,
                            Y = (float)(range * Math.Sin(fixedAngle.Value)) + host.Y,                     
                        };
                    else
                        if (isBomb)
                    {
                        target = new Position()
                        {
                            X = host.X,
                            Y = host.Y,
                        };
                    }
                    else
                    {
                        target = new Position()
                        {
                            X = player.X,
                            Y = player.Y,
                        };
                    }
                    if (!isBomb)
                    {
                        host.Owner.BroadcastPacketNearby(new ShowEffect()
                        {
                            EffectType = EffectType.Throw,
                            Color = new ARGB(color),
                            TargetObjectId = host.Id,
                            Pos1 = target,
                            AirTime = 1500
                        }, host, null);
                    }
                    host.Owner.Timers.Add(new WorldTimer(timer, (world, t) =>
                    {
                        world.BroadcastPacketNearby(new Aoe()
                        {
                            Pos = target,
                            Radius = radius,
                            Damage = (ushort)damage,
                            Duration = 0,
                            Effect = 0,
                            OrigType = host.ObjectType,
                            Color = new ARGB(color)
                        }, host, null);
                        world.AOE(target, radius, true, p =>
                        {
                            (p as IPlayer).Damage(damage, host);
                            if (!p.HasConditionEffect(ConditionEffects.Invincible) && 
                                !p.HasConditionEffect(ConditionEffects.Stasis))
                                p.ApplyConditionEffect(effect, effectDuration);
                        });
                    }));
                }
                cool = coolDown.Next(Random);
            }
            else
                cool -= time.ElaspedMsDelta;

            state = cool;
        }
    }
}
