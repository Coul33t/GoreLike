using GoreLike.Interfaces;
using RLNET;
using RogueSharp;

namespace GoreLike.Core {
    public class Actor : IActor, IDrawable {
        
        // IActor
        public string name { get; set; }
        public int awareness { get; set; }

        public int attack { get; set; }
        public int defense { get; set; }
        public int health { get; set; }
        public int max_health { get; set; }
        public int gold { get; set; }
        public int speed { get; set; }

        // IDrawable
        public RLColor colour { set; get; }
        public char symbol { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public void Draw(RLConsole console, IMap map) {
            if(!map.GetCell(x, y).IsExplored)
                return;

            if(map.IsInFov(x, y))
                console.Set(x, y, colour, Colours.FloorBackgroundFov, symbol);

            else
                console.Set(x, y, colour, Colours.FloorBackground, '.');

        }
    }
}
