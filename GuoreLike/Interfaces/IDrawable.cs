using RLNET;
using RogueSharp;

namespace GoreLike.Interfaces {
    public interface IDrawable {
        RLColor colour { set; get; }
        char symbol { get; set; }
        int x { get; set; }
        int y { get; set; }

        void Draw(RLConsole console, IMap map);
    }
}
