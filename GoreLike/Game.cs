﻿using RLNET;
using GoreLike.Core;
using GoreLike.Systems;
using RogueSharp.Random;
using System;

namespace GoreLike {
    public class Game {

        // The screen height and width are in number of tiles
        private static RLRootConsole _root_console;

        // The map console takes up most of the screen and is where the map will be drawn
        private static RLConsole _map_console;

        // Below the map console is the message console which displays attack rolls and other information 
        private static RLConsole _message_console;

        // The stat console is to the right of the map and display player and monster stats
        private static RLConsole _stat_console;

        // Above the map is the inventory console which shows the players equipment, abilities, and items
        private static RLConsole _inventory_console;

        public static DungeonMap dungeon_map {get; private set;}
        public static Player player { get; set; }
        public static CommandSystem command_system { get; private set; }
        public static MessageLog message_log { get; private set; }

        private static bool _render_required = true;

        public static IRandom random { get; private set; }

        public static void Main() {
            int seed = (int)DateTime.UtcNow.Ticks;
            random = new DotNetRandom(seed);

            string fontFileName = "Cheepicus_12x12.png";
            string consoleTitle = $"RougeSharp V3 Tutorial - Level 1 - Seed {seed}";

            // Tell RLNet to use the bitmap font that we specified and that each tile is 8 x 8 pixels
            _root_console = new RLRootConsole(fontFileName, Constants.screen_width, Constants.screen_height,
              12, 12, 1f, consoleTitle);

            _map_console = new RLConsole(Constants.map_width, Constants.map_height);
            _message_console = new RLConsole(Constants.message_width, Constants.message_height);
            _stat_console = new RLConsole(Constants.stat_width, Constants.stat_height);
            _inventory_console = new RLConsole(Constants.inventory_width, Constants.inventory_height);

            MapGenerator map_generator = new MapGenerator(Constants.map_width, Constants.map_height, 20, 7, 13);
            dungeon_map = map_generator.CreateMap();

            dungeon_map.UpdatePlayerFOV();

            command_system = new CommandSystem();

            // Set up a handler for RLNET's Update event
            _root_console.Update += OnRootConsoleUpdate;
            // Set up a handler for RLNET's Render event
            _root_console.Render += OnRootConsoleRender;

            message_log = new MessageLog();
            message_log.Add("The rogue arrive on level 1.");

            _inventory_console.SetBackColor(0, 0, Constants.inventory_width, Constants.inventory_height, Swatch.DbWood);
            _inventory_console.Print(1, 1, "Inventory", Colours.TextHeading);

            // Begin RLNET's game loop
            _root_console.Run();
        }

        // Event handler for RLNET's Update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e) {

            bool did_player_act = false;
            RLKeyPress key_press = _root_console.Keyboard.GetKeyPress();

            if (key_press != null) {
                if(key_press.Key == RLKey.Keypad8)
                    did_player_act = command_system.MovePlayer(Direction.up);
                else if(key_press.Key == RLKey.Keypad2)
                    did_player_act = command_system.MovePlayer(Direction.down);
                else if(key_press.Key == RLKey.Keypad4)
                    did_player_act = command_system.MovePlayer(Direction.left);
                else if(key_press.Key == RLKey.Keypad6)
                    did_player_act = command_system.MovePlayer(Direction.right);
                else if(key_press.Key == RLKey.Escape)
                    _root_console.Close();
            }

            if(did_player_act) {
                _render_required = true;
            }
                
        }

        // Event handler for RLNET's Render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e) {

            if(_render_required) {

                dungeon_map.Draw(_map_console);
                player.Draw(_map_console, dungeon_map);
                player.DrawStats(_stat_console);
                message_log.Draw(_message_console);

                RLConsole.Blit(_map_console, 0, 0, Constants.map_width, Constants.map_height,
                    _root_console, 0, Constants.inventory_height);
                RLConsole.Blit(_stat_console, 0, 0, Constants.stat_width, Constants.stat_height,
                    _root_console, Constants.map_width, 0);
                RLConsole.Blit(_message_console, 0, 0, Constants.message_width, Constants.message_height,
                    _root_console, 0, Constants.screen_height - Constants.message_height);
                RLConsole.Blit(_inventory_console, 0, 0, Constants.inventory_width, Constants.inventory_height,
                    _root_console, 0, 0);

                // Tell RLNET to draw the console that we set
                _root_console.Draw();

                _render_required = false;
            }
        }
    }
}