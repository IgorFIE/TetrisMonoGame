using Microsoft.Xna.Framework;

namespace Tetris.core {
    public class Position {
        public int x;
        public int y;
        public Rectangle positionRectangle;
        public Color color;

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
            color = Color.White;
            positionRectangle = new Rectangle(x * GameProperties.GAME_BLOCK_SIZE, y * GameProperties.GAME_BLOCK_SIZE, 
                GameProperties.GAME_BLOCK_SIZE, GameProperties.GAME_BLOCK_SIZE);
        }
    }
}
