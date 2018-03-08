using GoreLike.Interfaces;
using RLNET;
using RogueSharp;

namespace GoreLike.Core {
    class Actor : IActor, IDrawable {
        
        // IActor
        public string name { get; set; }
        public int awareness { get; set; }

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
