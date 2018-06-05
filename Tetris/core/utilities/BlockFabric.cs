using Microsoft.Xna.Framework;
using System;

namespace Tetris.core {
    class BlockFabric {

        private static Random random = new Random();

        private static int[,] oBlock = new int[,] { {0,0,0,0,0},
                                                    {0,0,0,0,0},
                                                    {0,1,1,0,0},
                                                    {0,1,1,0,0},
                                                    {0,0,0,0,0}};

        private static int[,] iBlock = new int[,] { {0,0,0,0,0},
                                                    {0,0,0,0,0},
                                                    {1,1,1,1,0},
                                                    {0,0,0,0,0},
                                                    {0,0,0,0,0}};

        private static int[,] sBlock = new int[,] { {0,0,0,0,0},
                                                    {0,0,0,0,0},
                                                    {0,0,1,1,0},
                                                    {0,1,1,0,0},
                                                    {0,0,0,0,0}};

        private static int[,] zBlock = new int[,] { {0,0,0,0,0},
                                                    {0,0,0,0,0},
                                                    {0,1,1,0,0},
                                                    {0,0,1,1,0},
                                                    {0,0,0,0,0}};

        private static int[,] lBlock = new int[,] { {0,0,0,0,0},
                                                    {0,0,0,0,0},
                                                    {0,1,1,1,0},
                                                    {0,1,0,0,0},
                                                    {0,0,0,0,0}};

        private static int[,] jBlock = new int[,] { {0,0,0,0,0},
                                                    {0,0,0,0,0},
                                                    {0,1,1,1,0},
                                                    {0,0,0,1,0},
                                                    {0,0,0,0,0}};

        private static int[,] tBlock = new int[,] { {0,0,0,0,0},
                                                    {0,0,0,0,0},
                                                    {0,1,1,1,0},
                                                    {0,0,1,0,0},
                                                    {0,0,0,0,0}};

        public static Block generateRandomBlock(Position[,] boardPositions) {
            switch (random.Next(0, 7)) {
                case 0:
                    return new Block(generateBlock(boardPositions,oBlock,Color.Yellow));
                case 1:
                    return new Block(generateBlock(boardPositions,iBlock,Color.Cyan));
                case 2:
                    return new Block(generateBlock(boardPositions,sBlock,Color.Red));
                case 3:
                    return new Block(generateBlock(boardPositions,zBlock,Color.Green));
                case 4:
                    return new Block(generateBlock(boardPositions,lBlock,Color.Orange));
                case 5:
                    return new Block(generateBlock(boardPositions,jBlock,Color.Blue));
                case 6:
                    return new Block(generateBlock(boardPositions,tBlock,Color.Purple));
            }
            return null;
        }

        private static Position[,] generateBlock(Position[,] boardPositions, int[,] blockBluePrint, Color color) {
            Position[,] positions = new Position[blockBluePrint.GetLength(0), blockBluePrint.GetLength(1)];
            for (int x = 0; x < blockBluePrint.GetLength(0); x++) {
                for (int y = 0; y < blockBluePrint.GetLength(1); y++) {
                    if (blockBluePrint[x, y] == 1) {
                        positions[x, y] = boardPositions[(boardPositions.GetLength(0)/2) + x -(blockBluePrint.GetLength(0)/2), y];
                        positions[x, y].color = color;
                    }
                }
            }
            return positions;
        }
    }
}
