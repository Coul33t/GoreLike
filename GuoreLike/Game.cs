using RLNET;
using GoreLike.Core;
using GoreLike.Systems;

namespace GoreLike {
    public class Game {

        // The screen height and width are in number of tiles
        private static readonly int _screen_width = 100;
        private static readonly int _screen_height = 70;
        private static RLRootConsole _root_console;

        // The map console takes up most of the screen and is where the map will be drawn
        private static readonly int _map_width = 80;
        private static readonly int _map_height = 48;
        private static RLConsole _map_console;

        // Below the map console is the message console which displays attack rolls and other information
        private static readonly int _message_width = 80;
        private static readonly int _message_height = 11;
        private static RLConsole _message_console;

        // The stat console is to the right of the map and display player and monster stats
        private static readonly int _stat_width = 20;
        private static readonly int _stat_height = 70;
        private static RLConsole _stat_console;

        // Above the map is the inventory console which shows the players equipment, abilities, and items
        private static readonly int _inventory_width = 80;
        private static readonly int _inventory_height = 11;
        private static RLConsole _inventory_console;

        public static DungeonMap dungeon_map {get; private set;}

        public static void Main() {
            string fontFileName = "terminal8x8.png";
            string consoleTitle = "RougeSharp V3 Tutorial - Level 1";
            // Tell RLNet to use the bitmap font that we specified and that each tile is 8 x 8 pixels
            _root_console = new RLRootConsole(fontFileName, _screen_width, _screen_height,
              8, 8, 1f, consoleTitle);

            _map_console = new RLConsole(_map_width, _map_height);
            _message_console = new RLConsole(_message_width, _message_height);
            _stat_console = new RLConsole(_stat_width, _stat_height);
            _inventory_console = new RLConsole(_inventory_width, _inventory_height);

            MapGenerator map_generator = new MapGenerator(_map_width, _map_height);
            dungeon_map = map_generator.CreateMap();

            // Set up a handler for RLNET's Update event
            _root_console.Update += OnRootConsoleUpdate;
            // Set up a handler for RLNET's Render event
            _root_console.Render += OnRootConsoleRender;
            // Begin RLNET's game loop
            _root_console.Run();
        }

        // Event handler for RLNET's Update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e) {
            _message_console.SetBackColor(0, 0, _message_width, _message_height, Swatch.DbDeepWater);
            _message_console.Print(1, 1, "Messages", Colours.TextHeading);

            _stat_console.SetBackColor(0, 0, _stat_width, _stat_height, Swatch.DbOldStone);
            _stat_console.Print(1, 1, "Stats", Colours.TextHeading);

            _inventory_console.SetBackColor(0, 0, _inventory_width, _inventory_height, Swatch.DbWood);
            _inventory_console.Print(1, 1, "Inventory", Colours.TextHeading);
        }

        // Event handler for RLNET's Render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e) {
            RLConsole.Blit(_map_console, 0, 0, _map_width, _map_height,
                _root_console, 0, _inventory_height);
            RLConsole.Blit(_stat_console, 0, 0, _stat_width, _stat_height,
                _root_console, _map_width, 0);
            RLConsole.Blit(_message_console, 0, 0, _message_width, _message_height,
                _root_console, 0, _screen_height - _message_height);
            RLConsole.Blit(_inventory_console, 0, 0, _inventory_width, _inventory_height,
                _root_console, 0, 0);

            dungeon_map.Draw(_map_console);

            // Tell RLNET to draw the console that we set
            _root_console.Draw();
        }
    }
}