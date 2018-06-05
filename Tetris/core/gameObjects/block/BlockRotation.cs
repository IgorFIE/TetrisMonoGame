using Tetris.core.interfaces;

namespace Tetris.core.utilities {
    class BlockRotation : IRotatable {

        private Position blockPivot;
        private Position[,] newRotationMatrix;
        private int newRotationColumn, newRotationRow, xRotationPos, yRotationPos;
        private bool invalidRotation;

        public Position[,] rotate(Position[,] boardPositions, Position[,] blocks) {
            initRotationVariables(blocks);
            for (int oldColumn = blocks.GetLength(1) - 1; oldColumn >= 0; oldColumn--) {
                resetRotationColumns(blocks);
                for (int oldRow = 0; oldRow < blocks.GetLength(0); oldRow++) {
                    if (blocks[oldRow, oldColumn] != null) {
                        invalidRotation = checkForInvalidRotations(boardPositions, blockPivot.x + xRotationPos, blockPivot.y + yRotationPos);
                        if (!invalidRotation) {
                            retrieveNewRotationBlock(boardPositions);
                        } else {
                            break;
                        }
                    }
                    incrementRotationColumns();
                }
                if (invalidRotation) {
                    break;
                }
                incrementRotationRows();
            }
            if (invalidRotation) {
                return blocks;
            }
            return newRotationMatrix;
        }

        private void retrieveNewRotationBlock(Position[,] boardPositions) {
            newRotationMatrix[newRotationRow, newRotationColumn] = boardPositions[blockPivot.x + xRotationPos, blockPivot.y + yRotationPos];
            newRotationMatrix[newRotationRow, newRotationColumn].color = blockPivot.color;
        }

        private void resetRotationColumns(Position[,] blocks) {
            newRotationColumn = 0;
            yRotationPos = -(blocks.GetLength(1) / 2);
        }

        private void incrementRotationColumns() {
            yRotationPos++;
            newRotationColumn++;
        }

        private void incrementRotationRows() {
            xRotationPos++;
            newRotationRow++;
        }

        private void initRotationVariables(Position[,] blocks) {
            newRotationMatrix = new Position[blocks.GetLength(1), blocks.GetLength(0)];
            blockPivot = blocks[blocks.GetLength(0) / 2, blocks.GetLength(1) / 2];
            newRotationColumn = 0; newRotationRow = 0; xRotationPos = -(blocks.GetLength(0) / 2); yRotationPos = 0;
            invalidRotation = false;
        }

        private bool checkForInvalidRotations(Position[,] boardPositions, int x, int y) {
            return x < 0 || x >= boardPositions.GetLength(0) || y >= boardPositions.GetLength(1);
        }
    }
}
