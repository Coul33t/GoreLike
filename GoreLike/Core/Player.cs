using RLNET;

namespace GoreLike.Core {
    public class Player : Actor {
        public Player() {
            name = "Maximilius";
            awareness = 15;

            attack = 3;
            defense = 2;
            health = 20;
            max_health = 20;
            gold = 10;
            speed = 1;

        colour = Colours.Player;
            symbol = '@';
            x = 10;
            y = 10;
        }

        public void DrawStats(RLConsole console) {
            console.Print(1, 1, $"Name:    {name}", Colours.Text);
            console.Print(1, 3, $"Health:  {health}/{max_health}", Colours.Text);
            console.Print(1, 5, $"Attack:  {attack}", Colours.Text);
            console.Print(1, 7, $"Defense: {defense}", Colours.Text);
            console.Print(1, 9, $"Gold:    {gold}", Colours.Gold);
        }
    }
}
