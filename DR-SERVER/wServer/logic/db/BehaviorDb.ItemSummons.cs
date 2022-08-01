using wServer.logic.behaviors;
using wServer.logic.loot;
using common.resources;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ ItemSummons = () => Behav()

              .Init("Invisible Object",
                new State(
                    new State("Init",
                        new TimedTransition(100, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
               .Init("Pentaract Eye Summon", //use mirrored chara xml it has no bugs
                new State(
                    new State("Summon",
                    new FamiliarFollow(),
                    new Wander(.5),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new ShootPlayer(0, 8, fixedAngle: 360 / 8, projectileIndex: 0, coolDown: 1000),
                    //new Follow(1.5, 10, 3),
                        new TimedTransition(6000,"Destroy")
                        ),
                    new State("Destroy",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Suicide()
                    )
            )
            )
               .Init("Mirrored Character",
                new State(
                    new State("Summon",
                    new Wander(.5),
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new Taunt(0.8, "01101110 01110101 01101100 01101100"),

                        new TimedTransition(7000, "Destroy")
                        ),
                    new State("Destroy",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Suicide()
                    )
            )
            )
         .Init("Mini Flying Brain", //big brain
                new State(
                    new State("Summon",
                    new TalismanAttack(10, ConditionEffectIndex.ArmorBroken, 4000, 100, 0xFFFFFF),
                    new FamiliarFollow(),//test
                        new TimedTransition(7000, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
        .Init("Bottled Lightning",
                new State(
                    new State("Init",
                        new Grenade(3.5, 0, coolDown: 1000, range: 10, isBomb: true, color: 0xAED6F1, effect: ConditionEffectIndex.Berserk),//bomb test
                        new TimedTransition(200, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
         .Init("EH Ability Bee 1", //orbit player
                new State(
                    new State("Summon",
                        new Wander(.5),
                    new StayCloseToSpawn(speed: .5, range: 3),
                    new TalismanAttack(120, ConditionEffectIndex.Curse, 4000, 1000),
                    new FamiliarFollow(),
                        new TimedTransition(6500, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
         .Init("EH Ability Bee 2", 
                new State(
                    new State("Summon",
                    new Wander(.5),
                    new StayCloseToSpawn(speed: .5, range: 3),
                    new TalismanAttack(250, ConditionEffectIndex.Curse, 4000, 1500),
                    new FamiliarFollow(),
                        new TimedTransition(6500, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
         .Init("EH Ability Bee 3", 
                new State(
                    new State("Summon",
                        new Wander(.5),
                    new StayCloseToSpawn(speed: .5, range: 3),
                    new TalismanAttack(100, ConditionEffectIndex.Curse, 4000, 1500),
                    new FamiliarFollow(),
                        new TimedTransition(6500, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
            ;

    }
}