using RLNET;
using RogueSharp;
using System.Collections.Generic;

namespace GoreLike.Core {
    public class DungeonMap : Map {

        private readonly List<Monster> _monsters;

        public List<Rectangle> rooms;

        public DungeonMap() {
            rooms = new List<Rectangle>();
            _monsters = new List<Monster>();
        }

        public void Draw(RLConsole map_console) {
            map_console.Clear();

            foreach(Cell cell in GetAllCells())
                SetConsoleSymbolForCell(map_console, cell);

            foreach(Monster monster in _monsters)
                monster.Draw(map_console, this);
        }

        public void AddPlayer(Player player) {
            Game.player = player;
            SetIsWalkable(player.x, player.y, false);
            UpdatePlayerFOV();
        }

        private void SetConsoleSymbolForCell(RLConsole map_console, Cell cell) {
            if(!cell.IsExplored)
                return;

            if (IsInFov(cell.X, cell.Y)) {
                if(cell.IsWalkable)
                    map_console.Set(cell.X, cell.Y, Colours.FloorFov, Colours.FloorBackgroundFov, '.');
                else
                    map_console.Set(cell.X, cell.Y, Colours.FloorFov, Colours.FloorBackgroundFov, '#');
            }

            else {
                if(cell.IsWalkable)
                    map_console.Set(cell.X, cell.Y, Colours.Floor, Colours.FloorBackground, '.');
                else
                    map_console.Set(cell.X, cell.Y, Colours.Floor, Colours.FloorBackgroundFov, '#');
            }

        }

        public void UpdatePlayerFOV() {
            Player player = Game.player;

            ComputeFov(player.x, player.y, player.awareness, true);

            foreach(Cell cell in GetAllCells()) {
                if(IsInFov(cell.X, cell.Y))
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
            }
        }

        public bool SetActorPosition(Actor actor, int x, int y) {

            if (GetCell(x, y).IsWalkable) {
                SetIsWalkable(actor.x, actor.y, true);

                actor.x = x;
                actor.y = y;

                SetIsWalkable(x, y, false);

                if(actor is Player)
                    UpdatePlayerFOV();

                return true;
            }

            return false;
        }

        public void SetIsWalkable(int x, int y, bool is_walkable) {
            Cell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, is_walkable, cell.IsExplored);
        }

        public void AddMonster(Monster monster) {
            _monsters.Add(monster);
            SetIsWalkable(monster.x, monster.y, false);
        }

        public Point GetWalkableTile(Rectangle room) {
            if (HasWalkableTile(room)) {
                for (int i = 0 ; i < 100 ; i++) {
                    int x = Game.random.Next(1, room.Width - 2) + room.X;
                    int y = Game.random.Next(1, room.Height - 2) + room.Y;

                    if(IsWalkable(x, y))
                        return new Point(x, y);
                }
            }

            return null;
        }

        public bool HasWalkableTile(Rectangle room) {
            for(int x = 1 ; x < room.Width - 1 ; x++)
                for(int y = 1 ; y < room.Height - 1 ; y++)
                    if(IsWalkable(x + room.X, y + room.Y))
                        return true;

            return false;
        }
    }
}
