namespace Tetris.core.interfaces {
    interface ICollidable {

        bool checkCollisionWithWalls(Position[,] board, Position[,] blocks, int objXPosition);

        bool checkCollisionWithWallsAndBottom(Position[,] board, Position[,] blocks, int objXPosition, int objYPosition);

        bool checkCollisionWithPlacedBlocks(Position[,] placedBlocks, Position[,] blocks, int objXPosition, int objYPosition);

        bool checkCollisionWithPlacedBlocks(Position[,] placedBlocks);
    }
}
