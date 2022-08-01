using wServer.logic.behaviors;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Misc = () => Behav()
            .Init("White Fountain",
                new State(
                    new HealPlayer(5, 1000, 200),
                    new HealPlayerMP(5, 1000, 200)
                    )
            )
            .Init("Sheep",
                new State(
                    new PlayerWithinTransition(15, "player_nearby"),
                    new State("player_nearby",
                        new Prioritize(
                            new StayCloseToSpawn(0.1, 2),
                            new Wander(0.1)
                            ),
                        new Taunt(0.001, 1000, "baa", "baa baa")
                        )
                    )
            )
        .Init("Nexus Crier",
                new State(
                    new PlayerWithinTransition(15, "player_nearby"),
                    new State("player_nearby",
                        new Prioritize(
                            new StayCloseToSpawn(0.1, 2),
                            new Wander(0.05)
                            ),
                        new Taunt(0.001,5000, "Hello!", "My name is Guill")
                        )
                    )
            )
           .InitMany("Black Cat", "Snowman", name =>
                new State(
                    new PetFollow(),
                    new Wander(0.05),
                    new HealPlayer(10, 3000, 150),
                    new HealPlayerMP(10, 3000, 100)
                )            
            );
    }
}