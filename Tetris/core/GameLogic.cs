namespace Tetris.core {
    class GameLogic {

        public Board board;
        public Position[,] placedBlocks;
        public Block currentBlock;
        public int score { get; private set; }
        public int lvl { get; private set; }
        public bool isGameOver { get; private set; }
        private int nextLvl;

        private int blockMovementDelay;
        private int movementDelayCounter;
        private int counterToRetrieveBlock;

        public GameLogic() {
            placedBlocks = new Position[GameProperties.BOARD_X_SIZE, GameProperties.BOARD_Y_SIZE];
            board = new Board(GameProperties.BOARD_X_SIZE, GameProperties.BOARD_Y_SIZE);
            blockMovementDelay = GameProperties.GAME_BLOCK_MOVEMENT;
            lvl = GameProperties.GAME_INITIAL_LVL;
            nextLvl = GameProperties.GAME_INITAL_LINES_TO_NEXT_LVL;
        }

        public void performGameLogicActions() {
            generateBlock();
            moveBlock();
            retrievePlacedBlocks();
            checkLineComplete();
        }

        private void checkLineComplete() {
            int lineCompleteCounter = 0;
            for (int y = 0; y < placedBlocks.GetLength(1); y++) {
                lineCompleteCounter = retrieveValueOfLineCompleted(lineCompleteCounter, y);
                if (lineCompleteCounter == placedBlocks.GetLength(0)) {
                    addScoreAndLevel();
                    cleanLine(y);
                    moveTopLines(y);
                }
            }
        }

        private void addScoreAndLevel() {
            score += lvl;
            if (score >= nextLvl) {
                lvl++;
                nextLvl = (score + lvl) * 2;
                if (blockMovementDelay > 3) {
                    blockMovementDelay -= 2;
                }
            }
        }

        private void moveTopLines(int y) {
            for (int yInvert = y - 1; yInvert >= 0; yInvert--) {
                for (int x = 0; x < placedBlocks.GetLength(0); x++) {
                    if (placedBlocks[x, yInvert] != null) {
                        board.boardPositions[x, yInvert + 1].color = placedBlocks[x, yInvert].color;
                        placedBlocks[x, yInvert] = null;
                        placedBlocks[x, yInvert + 1] = board.boardPositions[x, yInvert + 1];
                    }
                }
            }
        }

        private int retrieveValueOfLineCompleted(int lineCompleteCounter, int y) {
            lineCompleteCounter = 0;
            for (int x = 0; x < placedBlocks.GetLength(0); x++) {
                if (placedBlocks[x, y] != null) {
                    lineCompleteCounter++;
                } else {
                    break;
                }
            }
            return lineCompleteCounter;
        }

        private void cleanLine(int y) {
            for (int x = 0; x < placedBlocks.GetLength(0); x++) {
                placedBlocks[x, y] = null;
            }
        }

        private void moveBlock() {
            if (movementDelayCounter > blockMovementDelay) {
                movementDelayCounter = 0;
                currentBlock.moveDown(board.boardPositions, placedBlocks);
            } else {
                movementDelayCounter++;
            }
        }

        private void generateBlock() {
            if (currentBlock == null) {
                currentBlock = BlockFabric.generateRandomBlock(board.boardPositions);
            }
        }

        private void retrievePlacedBlocks() {
            if (currentBlock != null) {
                if (canRetrieveBlock()) {
                    if (counterToRetrieveBlock > GameProperties.GAME_RETRIEVE_BLOCK_DELAY) {
                        placeBlocks();
                        currentBlock = null;
                        counterToRetrieveBlock = 0;
                    } else {
                        counterToRetrieveBlock++;
                    }
                } else {
                    counterToRetrieveBlock = 0;
                }
            }
        }

        private void placeBlocks() {
            foreach (Position pos in currentBlock.blocks) {
                if (pos != null) {
                    if (pos.y == GameProperties.GAME_OVER_Y) {
                        isGameOver = true;
                    }
                    placedBlocks[pos.x, pos.y] = pos;
                }
            }
        }

        private bool canRetrieveBlock() {
            foreach (Position pos in currentBlock.blocks) {
                if (pos == null) {
                    continue;
                } else if (pos.y == board.boardPositions.GetLength(1) - 1) {
                    return true;
                }
                if (placedBlocks[pos.x, pos.y + 1] != null) {
                    return true;
                }
            }
            return false;
        }
    }
}
