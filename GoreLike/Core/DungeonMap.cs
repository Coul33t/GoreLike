using RLNET;
using RogueSharp;

namespace GoreLike.Core {
    public class DungeonMap : Map {
        public void Draw(RLConsole map_console) {
            map_console.Clear();

            foreach(Cell cell in GetAllCells()) {
                SetConsoleSymbolForCell(map_console, cell);
            }
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
    }
}
