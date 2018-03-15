using GoreLike.Core;
using RogueSharp.DiceNotation;

namespace GoreLike.Monsters {
    public class Zombie : Monster {
        public static Zombie Create(int level) {
            int _health = Dice.Roll("2D3");
            return new Zombie {
                name = "Zombie",
                awareness = 15,

                attack = 1 + Dice.Roll("1D1"),
                defense = 0,
                health = _health,
                max_health = _health,
                gold = 1,
                speed = Dice.Roll("1D10") / 10,
                
                colour = Colours.ZombieColor          
            };
        }
    }
}
