using System;
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
    }
}
