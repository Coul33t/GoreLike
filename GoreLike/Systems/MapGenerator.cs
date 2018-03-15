using GoreLike.Core;
using RogueSharp;
using System;
using System.Linq;

namespace GoreLike.Systems {
    class MapGenerator {
        private readonly int _width;
        private readonly int _height;

        private readonly int _max_rooms;
        private readonly int _room_min_size;
        private readonly int _room_max_size;

        private readonly DungeonMap _map;

        public MapGenerator(int width, int height, 
                            int max_rooms, int room_min_size,
                            int room_max_size) {
            _width = width;
            _height = height;
            _max_rooms = max_rooms;
            _room_min_size = room_min_size;
            _room_max_size = room_max_size;
            _map = new DungeonMap();
        }

        private void PlacePlayer() {
            Player player = Game.player;

            if(player == null)
                player = new Player();

            player.x = _map.rooms[0].Center.X;
            player.y = _map.rooms[0].Center.Y;

            _map.AddPlayer(player);
        }

        public DungeonMap CreateMap() {
            _map.Initialize(_width, _height);

            for (int i = 0 ; i < _max_rooms ; i++) {
                int room_width = Game.random.Next(_room_min_size, _room_max_size);
                int room_height = Game.random.Next(_room_min_size, _room_max_size);
                int room_x_position = Game.random.Next(0, _width - room_width - 1);
                int room_y_position = Game.random.Next(0, _height - room_height - 1);

                Rectangle new_room = new Rectangle(room_x_position, room_y_position, room_width, room_height);

                bool new_room_interstects = _map.rooms.Any(room => new_room.Intersects(room));

                if(!new_room_interstects)
                    _map.rooms.Add(new_room);
            }

            foreach(Rectangle room in _map.rooms)
                CreateRoom(room);
            // = 1 because we connect the current one with the previous one
            for (int i = 1 ; i < _map.rooms.Count ; i++) {
                int previous_room_center_x = _map.rooms[i - 1].Center.X;
                int previous_room_center_y = _map.rooms[i - 1].Center.Y;
                int current_room_center_x = _map.rooms[i].Center.X;
                int current_room_center_y = _map.rooms[i].Center.Y;

                if(Game.random.Next(1, 2) == 1) {
                    CreateHorizontalTunnel(previous_room_center_x, current_room_center_x, previous_room_center_y);
                    CreateVerticalTunnel(previous_room_center_y, current_room_center_y, current_room_center_x);
                }
                else {
                    CreateVerticalTunnel(previous_room_center_y, current_room_center_y, previous_room_center_x);
                    CreateHorizontalTunnel(previous_room_center_x, current_room_center_x, current_room_center_y);
                }
            }

            PlacePlayer();

            return _map;
        }

        private void CreateRoom(Rectangle room) {
            for(int x = room.Left + 1 ; x < room.Right ; x++)
                for(int y = room.Top + 1 ; y < room.Bottom ; y++)
                    _map.SetCellProperties(x, y, true, true, true);
        }

        // Carve a tunnel out of the map parallel to the x-axis
        private void CreateHorizontalTunnel(int x_start, int x_end, int y_position) {
            for(int x = Math.Min(x_start, x_end) ; x <= Math.Max(x_start, x_end) ; x++)
                _map.SetCellProperties(x, y_position, true, true);
        }

        // Carve a tunnel out of the map parallel to the y-axis
        private void CreateVerticalTunnel(int y_start, int y_end, int x_position) {
            for(int y = Math.Min(y_start, y_end) ; y <= Math.Max(y_start, y_end) ; y++)
                _map.SetCellProperties(x_position, y, true, true);
        }

        private void PlaceMonsters() {
            foreach(var room in _map.rooms) {

            }
        }
    }
}
