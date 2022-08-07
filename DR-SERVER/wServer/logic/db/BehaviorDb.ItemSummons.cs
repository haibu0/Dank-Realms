using wServer.logic.behaviors;
using wServer.logic.loot;
using common.resources;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ ItemSummons = () => Behav()

        .Init("Fungal Mushroom",
                new State(
                    new State("Init",
                    new PetBomb(damage: 1000, radius: 3, coolDown: 600, color: 10066431),
                        new TimedTransition(6100, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
              .Init("Invisible Object",
                new State(
                    new State("Init",
                        new TimedTransition(10, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )

            .Init("Fake White Bag",
                new State(
                    new State("Init",
                        new TimedTransition(10000, "Destroy")
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
                   new Wander(.3),
                    new FamiliarFollow(),//test
                    new PetBomb(damage: 50, radius: 3, coolDown: 500, color: 6947071),
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
                    new PetBomb(damage: 1100, radius: 3, effect: ConditionEffectIndex.Stasis, effDuration: 4, coolDown: 100, color: 0xFFFF00),
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
                    new TalismanAttack(120, ConditionEffectIndex.Curse, range: 6, 4000, 1000),
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
                    new TalismanAttack(250, ConditionEffectIndex.Curse, range: 3, duration: 4000, coolDown: 1500),
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
                    new TalismanAttack(100, ConditionEffectIndex.Curse, range: 3, 4000, 1500),
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