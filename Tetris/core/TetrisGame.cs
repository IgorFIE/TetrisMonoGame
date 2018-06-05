using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tetris.core;

namespace Tetris {
    public class TetrisGame : Game {
        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;
        private Texture2D defaultTexture;
        private SpriteFont arialFont;

        private GameControls controls;
        private GameLogic gameLogic;
        private int endGameDelayCounter;

        public TetrisGame() {
            initGraphics();
            Content.RootDirectory = "Content";
        }

        private void initGraphics() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameProperties.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = GameProperties.SCREEN_HEIGHT;
            graphics.ApplyChanges();
        }

        protected override void Initialize() {
            initGameVariables();
            initDefaultTexture();
            base.Initialize();
        }

        private void initGameVariables() {
            controls = new GameControls();
            gameLogic = new GameLogic();
        }

        private void initDefaultTexture() {
            defaultTexture = new Texture2D(GraphicsDevice, 1, 1);
            defaultTexture.SetData(new[] { Color.White });
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arialFont = Content.Load<SpriteFont>("Fonts/Arial");
        }

        protected override void Update(GameTime gameTime) {
            handleEscape();
            controls.handlePlayerInput(gameLogic);
            gameLogic.performGameLogicActions();
            base.Update(gameTime);
        }

        private void handleEscape() {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || gameLogic.isGameOver) {
                if (endGameDelayCounter < GameProperties.END_GAME_DELAY) {
                    Exit();
                } else {
                    endGameDelayCounter++;
                }
            }
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            drawNewScreen();
            base.Draw(gameTime);
        }

        private void drawNewScreen() {
            spriteBatch.Begin();
            drawGameObjects();
            spriteBatch.DrawString(arialFont, "Score: " + gameLogic.score + " || Lvl: " + gameLogic.lvl, 
                new Vector2(GameProperties.GAME_BLOCK_SIZE, GameProperties.GAME_BLOCK_SIZE), Color.White);
            spriteBatch.End();
        }

        private void drawGameObjects() {
            drawBoard();
            drawMaximLine();
            drawCurrentBlock();
            drawPlacedBlocks();
        }

        private void drawPlacedBlocks() {
            foreach (Position pos in gameLogic.placedBlocks) {
                if (pos != null) {
                    spriteBatch.Draw(defaultTexture, pos.positionRectangle, pos.color);
                }
            }
        }

        private void drawCurrentBlock() {
            if (gameLogic.currentBlock != null) {
                foreach (Position pos in gameLogic.currentBlock.blocks) {
                    if (pos != null) {
                        spriteBatch.Draw(defaultTexture, pos.positionRectangle, pos.color);
                    }
                }
            }
        }

        private void drawBoard() {
            foreach (Position pos in gameLogic.board.boardPositions) {
                spriteBatch.Draw(defaultTexture, pos.positionRectangle, Color.Black);
            }
        }

        private void drawMaximLine() {
            for (int x = 0; x < gameLogic.board.boardPositions.GetLength(0); x++) {
                spriteBatch.Draw(defaultTexture, gameLogic.board.boardPositions[x,GameProperties.GAME_OVER_Y].positionRectangle, Color.DarkGray);
            }
        }
    }
}
