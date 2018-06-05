using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.core.interfaces;

namespace Tetris.core.gameObjects.block {
    class BlockCollision : ICollidable {

        private Position fakePosition;
        private int fakeX;
        private int fakeY;

        public BlockCollision() {
            fakePosition = new Position(0, 0);
        }

        public bool checkCollisionWithWalls(Position[,] board, Position[,] blocks, int objXPosition) {
            return checkCollisionWithWallsAndBottom(board, blocks, objXPosition, 0);
        }

        public bool checkCollisionWithWallsAndBottom(Position[,] board, Position[,] blocks, int objXPosition, int objYPosition) {
            foreach (Position pos in blocks) {
                if (pos != null) {
                    populateFakePositions(objXPosition, objYPosition, pos);
                    if (fakeX < 0 || fakeX >= board.GetLength(0) || fakeY >= board.GetLength(1)) {
                        return true;
                    }
                }
            }
            return false;
        }

        private void populateFakePositions(int objXPosition, int objYPosition, Position pos) {
            fakeX = pos.x + objXPosition;
            fakeY = pos.y + objYPosition;
        }

        public bool checkCollisionWithPlacedBlocks(Position[,] placedBlocks, Position[,] blocks, int objXPosition, int objYPosition) {
            foreach (Position pos in blocks) {
                if (pos != null) {
                    populateFakeRectangle(objXPosition, objYPosition, pos);
                    if (checkCollisionWithPlacedBlocks(placedBlocks)) {
                        return true;
                    }
                }
            }
            return false;
        }

        private void populateFakeRectangle(int x, int y, Position pos) {
            fakePosition.x = pos.x + x;
            fakePosition.y = pos.y + y;
            fakePosition.positionRectangle.X = pos.positionRectangle.X + (x * GameProperties.GAME_BLOCK_SIZE);
            fakePosition.positionRectangle.Y = pos.positionRectangle.Y + (y * GameProperties.GAME_BLOCK_SIZE);
        }

        public bool checkCollisionWithPlacedBlocks(Position[,] placedBlocks) {
            for (int x = GameUtilities.retrieveLeftValue(fakePosition.x, 1); x < GameUtilities.retrieveRightValue(fakePosition.x, 1); x++) {
                for (int y = GameUtilities.retrieveTopValue(fakePosition.y, 1); y < GameUtilities.retrieveBottomValue(fakePosition.y, 1); y++) {
                    if (placedBlocks[x, y] != null) {
                        if (fakePosition.positionRectangle.Intersects(placedBlocks[x, y].positionRectangle)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
