using Microsoft.Xna.Framework.Input;

namespace Tetris.core {
    class GameControls {
        private int movePieceDelay;
        private int rotatePieceDelay;

        private int rotateCounter;
        private int moveCounter;

        public GameControls() {
            movePieceDelay = GameProperties.PLAYER_MOVEMENT_DELAY;
            rotatePieceDelay = GameProperties.PLAYER_ROTATION_DELAY;
        }

        public void handlePlayerInput(GameLogic gameLogic) {
            handlePlayerMovements(gameLogic);
            handlePieceRotationInput(gameLogic);
        }

        private void handlePieceRotationInput(GameLogic gameLogic) {
            if (rotateCounter > rotatePieceDelay) {
                if (handleKeyPressed(gameLogic, Keys.Space)) {
                    rotateCounter = 0;
                }
            } else {
                rotateCounter++;
            }
        }

        private void handlePlayerMovements(GameLogic gameLogic) {
            if (moveCounter > movePieceDelay) {
                handleKeyPressed(gameLogic, Keys.Left);
                handleKeyPressed(gameLogic, Keys.Right);
                handleKeyPressed(gameLogic, Keys.Down);
            } else {
                moveCounter++;
            }
        }

        private bool handleKeyPressed(GameLogic gameLogic, Keys key) {
            if (Keyboard.GetState().IsKeyDown(key)) {
                moveCounter = 0;
                if (gameLogic.currentBlock != null) {
                    performAction(gameLogic, key);
                }
                return true;
            }
            return false;
        }

        private void performAction(GameLogic gameLogic, Keys key) {
            switch (key) {
                case Keys.Space:
                    gameLogic.currentBlock.rotate(gameLogic.board.boardPositions, gameLogic.placedBlocks);
                    break;
                case Keys.Down:
                    gameLogic.currentBlock.moveDown(gameLogic.board.boardPositions, gameLogic.placedBlocks);
                    break;
                case Keys.Left:
                    gameLogic.currentBlock.moveLeft(gameLogic.board.boardPositions, gameLogic.placedBlocks);
                    break;
                case Keys.Right:
                    gameLogic.currentBlock.moveRight(gameLogic.board.boardPositions, gameLogic.placedBlocks);
                    break;
            }
        }
    }
}
