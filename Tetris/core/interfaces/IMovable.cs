using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.core.interfaces {
    interface IMovable {
        void moveDown(Position[,] boardPositions, Position[,] placedBlocks);
        void moveLeft(Position[,] boardPositions, Position[,] placedBlocks);
        void moveRight(Position[,] boardPositions, Position[,] placedBlocks);
    }
}
