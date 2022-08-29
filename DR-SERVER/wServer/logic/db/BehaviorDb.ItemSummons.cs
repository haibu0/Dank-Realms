using wServer.logic.behaviors;
using wServer.logic.loot;
using common.resources;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ ItemSummons = () => Behav()
        .Init("AoO Skull Minion",
                new State(
                    new State("Summon",
                        new Wander(.5),
                    new TalismanAttack(200, ConditionEffectIndex.Curse, range: 6, 4000, 500, color: 0),
                    new FamiliarFollow(),
                        new TimedTransition(6500, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
        .Init("AoOPoison Cluster0",
                new State(
                    new State("Init",                
                        new TossObject(child: "Invisible Object", range: 2, angle: 0, coolDown: 9000, airTime: 500, color: 16711680),
                        new TossObject(child: "Invisible Object", range: 2, angle: 180, coolDown: 9000, airTime: 500, color: 16711680),
                        new TossObject(child: "Invisible Object", range: 2, angle: -90, coolDown: 9000, airTime: 500, color: 16711680),
                        new TossObject(child: "Invisible Object", range: 2, angle: 90, coolDown: 9000, airTime: 500, color: 16711680),

                        new TimedTransition(100, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
        .Init("AoOPoison Cluster1",
                new State(
                    new State("Init",
                        new MoveTo2(2, 0, speed: 1, isMapPosition: false, instant: true),
                        new TimedTransition(1500, "hit")
                        ),
                    new State("hit",
                        new PetBomb(damage: 550, radius: 3, coolDown: 9999, color: 16711680),
                        new Suicide()
                    )
            )
            )
        .Init("AoOPoison Cluster2",
                new State(
                    new State("Init",
                        new MoveTo2(-2, 0, speed: 1, isMapPosition: false, instant: true),
                        new TimedTransition(1500, "hit")
                        ),
                    new State("hit",
                        new PetBomb(damage: 550, radius: 3, coolDown: 9999, color: 16711680),
                        new Suicide()
                    )
            )
            )
        .Init("AoOPoison Cluster3",
                new State(
                    new State("Init",
                        new MoveTo2(0, -2, speed: 1, isMapPosition: false, instant: true),
                        new TimedTransition(1500, "hit")
                        ),
                    new State("hit",
                        new PetBomb(damage: 550, radius: 3, coolDown: 9999, color: 16711680),
                        new Suicide()
                    )
            )
            )
        .Init("AoOPoison Cluster4",
                new State(
                    new State("Init",
                        new MoveTo2(0, 2, speed: 1, isMapPosition: false, instant: true),
                        new TimedTransition(1500, "hit")
                        ),
                    new State("hit",
                        new PetBomb(damage: 550, radius: 3, coolDown: 9999, color: 16711680),
                        new Suicide()
                    )
            )
            )
        .Init("Genesis 1",
                new State(
                    new State("Init",
                    new MoveTo2(5, 0, speed: 1, isMapPosition: false),
                    new PetBomb(damage: 250, radius: 6, coolDown: 400, color: 6947071),
                        new TimedTransition(4000, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
        .Init("Genesis 2",
                new State(
                    new State("Init",
                    new MoveTo2(0, 5, speed: 1, isMapPosition: false),
                    new PetBomb(damage: 250, radius: 6, coolDown: 400, color: 6947071),
                        new TimedTransition(4000, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
        .Init("Genesis 3",
                new State(
                    new State("Init",
                    new MoveTo2(-5, 0, speed: 1, isMapPosition: false),
                    new PetBomb(damage: 250, radius: 6, coolDown: 400, color: 6947071),
                        new TimedTransition(4000, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
        .Init("Genesis 4",
                new State(
                    new State("Init",
                    new MoveTo2(0, -5, speed: 1, isMapPosition: false),
                    new PetBomb(damage: 250, radius: 6, coolDown: 400, color: 6947071),
                        new TimedTransition(4000, "Destroy")
                        ),
                    new State("Destroy",
                        new Suicide()
                    )
            )
            )
        .Init("Fungal Mushroom",
                new State(
                    new State("Init",
                    new PetBomb(damage: 1000, radius: 3, coolDown: 1200, color: 10066431),
                        new TimedTransition(4800, "Destroy")
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
                    new PetBomb(damage: 1100, radius: 3, effect: ConditionEffectIndex.Stasis, effDuration: 4, coolDown: 1000, color: 0xFFFF00),
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
                    new TalismanAttack(120, ConditionEffectIndex.Curse, range: 4, 4000, 1000),
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
                    new TalismanAttack(250, ConditionEffectIndex.Curse, range: 4, duration: 4000, coolDown: 1500),
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
                    new TalismanAttack(100, ConditionEffectIndex.Curse, range: 4, 4000, 1500),
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