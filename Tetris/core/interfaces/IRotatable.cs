namespace Tetris.core.interfaces {
    interface IRotatable {
        Position[,] rotate(Position[,] boardPositions, Position[,] blocks);
    }
}
