namespace Tetris.core {
    public class Board {

        public Position[,] boardPositions { get; private set; }

        public Board(int x, int y) {
            boardPositions = new Position[x, y];
            generateBoardPositions(x, y);
        }

        private void generateBoardPositions(int xSize, int ySize) {
            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++) {
                    boardPositions[x, y] = new Position(x, y);
                }
            }
        }
    }
}
