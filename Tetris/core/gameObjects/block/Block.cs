using System;
using Tetris.core.gameObjects.block;
using Tetris.core.interfaces;
using Tetris.core.utilities;

namespace Tetris.core {
    public class Block : IMovable {
        public Position[,] blocks { get; private set; }
        private IRotatable blockRotation;
        private ICollidable blockCollision;

        private bool hasCollisionWithPlacedBlocks;
        private bool hasCollisionWithWalls;

        public Block(Position[,] block) {
            this.blocks = block;
            blockRotation = new BlockRotation();
            blockCollision = new BlockCollision();
        }

        public void moveDown(Position[,] boardPositions, Position[,] placedBlocks) {
            hasCollisionWithPlacedBlocks = blockCollision.checkCollisionWithPlacedBlocks(placedBlocks, blocks, 0, 1);
            hasCollisionWithWalls = blockCollision.checkCollisionWithWallsAndBottom(boardPositions, blocks, 0, 1);
            moveBlocksToNewPositions(boardPositions, 0, 1);
        }

        public void moveLeft(Position[,] boardPositions, Position[,] placedBlocks) {
            hasCollisionWithPlacedBlocks = blockCollision.checkCollisionWithPlacedBlocks(placedBlocks, blocks, -1, 0);
            hasCollisionWithWalls = blockCollision.checkCollisionWithWalls(boardPositions, blocks, -1);
            moveBlocksToNewPositions(boardPositions, -1, 0);
        }

        public void moveRight(Position[,] boardPositions, Position[,] placedBlocks) {
            hasCollisionWithPlacedBlocks = blockCollision.checkCollisionWithPlacedBlocks(placedBlocks, blocks, 1, 0);
            hasCollisionWithWalls = blockCollision.checkCollisionWithWalls(boardPositions, blocks, 1);
            moveBlocksToNewPositions(boardPositions, 1, 0);
        }

        private void moveBlocksToNewPositions(Position[,] boardPositions, int x, int y) {
            if (!hasCollisionWithPlacedBlocks) {
                for (int xPos = 0; xPos < blocks.GetLength(0); xPos++) {
                    for (int yPos = 0; yPos < blocks.GetLength(1); yPos++) {
                        if (blocks[xPos, yPos] != null) {
                            if (!hasCollisionWithWalls) {
                                boardPositions[blocks[xPos, yPos].x + x, blocks[xPos, yPos].y + y].color = blocks[xPos, yPos].color;
                                blocks[xPos, yPos] = boardPositions[blocks[xPos, yPos].x + x, blocks[xPos, yPos].y + y];
                            }
                        }
                    }
                }
            }
            hasCollisionWithWalls = false;
        }

       

        public void rotate(Position[,] boardPositions, Position[,] placedBlocks) {
            if (canPerformRotation(boardPositions, placedBlocks)) {
                blocks = blockRotation.rotate(boardPositions,blocks);
            }
        }

        private bool canPerformRotation(Position[,] boardPositions, Position[,] placedBlocks) {
            return !blockCollision.checkCollisionWithWallsAndBottom(boardPositions, blocks, 1, 1) && 
                    !blockCollision.checkCollisionWithWallsAndBottom(boardPositions, blocks, -1, 1) &&
                    !blockCollision.checkCollisionWithPlacedBlocks(placedBlocks, blocks, 0, 1) && 
                    !blockCollision.checkCollisionWithPlacedBlocks(placedBlocks, blocks, -1, 0) && 
                    !blockCollision.checkCollisionWithPlacedBlocks(placedBlocks, blocks, 1, 0);
        }
    }
}
