using wServer.logic.behaviors;
using wServer.logic.loot;
using common.resources;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ ItemSummons = () => Behav()

              
               .Init("Pentaract Eye Summon",
                new State(
                    new State("Summon",
                    new PetFollow(),
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
            ;

    }
}