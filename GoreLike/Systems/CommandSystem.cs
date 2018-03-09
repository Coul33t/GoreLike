using GoreLike.Core;

namespace GoreLike.Systems {
    public class CommandSystem {
        public bool MovePlayer(Direction direction) {
            int x = Game.player.x;
            int y = Game.player.y;

            switch(direction) {
                case Direction.up: {
                    y = Game.player.y - 1;
                    break;
                }
                case Direction.down: {
                    y = Game.player.y + 1;
                    break;
                }
                case Direction.left: {
                    x = Game.player.x - 1;
                    break;
                }
                case Direction.right: {
                    x = Game.player.x + 1;
                    break;
                }
                default: {
                    return false;
                }        
            }

            if(Game.dungeon_map.SetActorPosition(Game.player, x, y))
                return true;

            return false;
        }
    }
}
