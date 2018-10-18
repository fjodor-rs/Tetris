using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;

/// <summary>
/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
/// </summary>
class GameWorld
{
    /// <summary>
    /// An enum for the different game states that the game can have.
    /// </summary>
    enum GameState
    {
        Init,
        Playing,
        GameOver
    }

    enum GameType
    {
        Normal,
        Hard
    }

    public static int score = 0;
    public static int rowsToGo = 20;
    public static int level = 1;

    /// <summary>
    /// The random-number generator of the game.
    /// </summary>
    public static Random Random { get { return random; } }
    static Random random;

    /// <summary>
    /// The main font of the game.
    /// </summary>
    SpriteFont font, menuFont;

    /// <summary>
    /// The current game state.
    /// </summary>
    GameState gameState;

    GameType gameType;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    TetrisGrid grid;
    TetrisGame game;
    Texture2D background, emptyCell, endScreen;
    Vector2 mousePos;
    Song normalTheme, hardTheme;
    static SoundEffect nock, rowDel, lvlUp;

    TetrisBlock tetrisBlock, drawBlock;
    int nextBlock, currentBlock, dropSpeed, previousTime, delay, nrBlocks;
    TetrisBlock.Block blockType;
    Color blockColor;
	double timePressed;

    public GameWorld(TetrisGame game)
    {
        this.game = game;
        random = new Random();
        gameState = GameState.Init;
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        menuFont = TetrisGame.ContentManager.Load<SpriteFont>("MenuFont");
        background = TetrisGame.ContentManager.Load<Texture2D>("TETRIS");
        endScreen = TetrisGame.ContentManager.Load<Texture2D>("Gameover");
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        normalTheme = TetrisGame.ContentManager.Load<Song>("Normaltheme");
        hardTheme = TetrisGame.ContentManager.Load<Song>("Hardtheme");
        nock = TetrisGame.ContentManager.Load<SoundEffect>("nock");
        rowDel = TetrisGame.ContentManager.Load<SoundEffect>("rowDel");
        lvlUp = TetrisGame.ContentManager.Load<SoundEffect>("lvlUp");
        MediaPlayer.IsRepeating = true;
        grid = new TetrisGrid();
        nrBlocks = 7;
        nextBlock = Random.Next(nrBlocks);
        ResetBlock();
        dropSpeed = 1000;
        previousTime = 0;
		timePressed = 0;
    }

    public void ResetBlock()
    {
        currentBlock = nextBlock;
        nextBlock = Random.Next(nrBlocks);
        BlockIndex(currentBlock);
        tetrisBlock = new TetrisBlock(blockType, blockColor, grid);
        BlockIndex(nextBlock);
        drawBlock = new TetrisBlock(blockType, blockColor);
        drawBlock.Position = new Point(13, 4);
		timePressed = 0;
    }

    public void BlockIndex(int block)
    {
        switch (block)
        {
            case 0:
                blockType = TetrisBlock.Block.I;
                blockColor = Color.Aqua;
                break;
            case 1:
                blockType = TetrisBlock.Block.J;
                blockColor = Color.Yellow;
                break;
            case 2:
                blockType = TetrisBlock.Block.L;
                blockColor = Color.RoyalBlue;
                break;
            case 3:
                blockType = TetrisBlock.Block.O;
                blockColor = Color.Coral;
                break;
            case 4:
                blockType = TetrisBlock.Block.S;
                blockColor = Color.Crimson;
                break;
            case 5:
                blockType = TetrisBlock.Block.Z;
                blockColor = Color.DeepPink;
                break;
            case 6:
                blockType = TetrisBlock.Block.T;
                blockColor = Color.ForestGreen;
                break;
            case 7:
                blockType = TetrisBlock.Block.Rod;
                blockColor = Color.OrangeRed;
                break;
            case 8:
                blockType = TetrisBlock.Block.Cup;
                blockColor = Color.MediumPurple;
                break;
            case 9:
                blockType = TetrisBlock.Block.Mill;
                blockColor = Color.DodgerBlue;
                break;
        }
    }

    void MoveDown()
    {
        previousTime = 0;
        tetrisBlock.Position += new Point(0, 1);
        if (tetrisBlock.BottomBounds())
        {
            if (tetrisBlock.Position == new Point(3, 0))
                gameState = GameState.GameOver;
            tetrisBlock.Position += new Point(0, -1);
            tetrisBlock.BlockToGrid();
            ResetBlock();
			grid.LineCheck();
            nock.Play();
        }
    }

    public static void RowDelSound()
    {
        rowDel.Play();
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (gameState == GameState.Playing)
        {

            //Met de klok mee draaien
            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
            {
                tetrisBlock.Rotate();
                if (tetrisBlock.BottomBounds())
                {
                    tetrisBlock.Rotate(false);
                }
                else
                {
                    while (tetrisBlock.SideBounds())
                    {
                        if (tetrisBlock.Position.X < 5)
                            tetrisBlock.Position += new Point(1, 0);
                        else
                            tetrisBlock.Position += new Point(-1, 0);
                    }
                }
            }

            //Tegen de klok in draaien
            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                tetrisBlock.Rotate(false);
                if (tetrisBlock.BottomBounds())
                {
                    tetrisBlock.Rotate();
                }
                else
                {
                    while (tetrisBlock.SideBounds())
                    {
                        if (tetrisBlock.Position.X < 5)
                            tetrisBlock.Position += new Point(1, 0);
                        else
                            tetrisBlock.Position += new Point(-1, 0);
                    }
                }
            }

            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.A))
            {
                tetrisBlock.Position -= new Point(1, 0);
                if (tetrisBlock.SideBounds())
                    tetrisBlock.Position -= new Point(-1, 0);

            }

            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.D))
            {
                tetrisBlock.Position += new Point(1, 0);
                if (tetrisBlock.SideBounds())
                    tetrisBlock.Position += new Point(-1, 0);
            }
            if (inputHelper.KeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                timePressed += gameTime.ElapsedGameTime.TotalMilliseconds;
                delay += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timePressed >= 300 && delay >= 100)
                {
                    MoveDown();
                    delay = 0;
                }

                if (!inputHelper.KeyDown(Microsoft.Xna.Framework.Input.Keys.S))
                    timePressed = 0;

            }
            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.S))
            {
                MoveDown();
            }
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.R))
            Reset();

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
            game.Quit();

        if (gameState == GameState.Init)
        {
            mousePos = inputHelper.MousePosition;
            if (inputHelper.MouseLeftButtonPressed())
            {
                gameState = GameState.Playing;
                if (mousePos.X < TetrisGame.ScreenSize.X / 2)
                {
                    gameType = GameType.Normal;
                    MediaPlayer.Play(normalTheme);
                    nrBlocks = 7;
                    dropSpeed = 1000;
                }
                else
                {
                    gameType = GameType.Hard;
                    MediaPlayer.Play(hardTheme);
                    nrBlocks = 10;
                    dropSpeed = 600;

                }

                //Twee keer ResetBlock om een nieuwe waarde voor block en nextblock te krijgen, die bij de goede gamemode hoort
                ResetBlock();
                ResetBlock();
            }
        }
    }

    public void Update(GameTime gameTime)
    {
        if (gameState == GameState.Playing)
        {
            previousTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (previousTime >= dropSpeed)
            {
                MoveDown();
            }

            if (rowsToGo <= 0)
            {
                level += 1;
                rowsToGo += 20;
                lvlUp.Play();
                if (dropSpeed > 400)
                    dropSpeed -= 100;
            }
        }
        
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        if (gameState == GameState.Playing)
        {
            grid.Draw(gameTime, spriteBatch);
            tetrisBlock.Draw(gameTime, spriteBatch);
            drawBlock.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "Score = " + score, new Vector2(400, 275), Color.Black);
            spriteBatch.DrawString(font, "Rows till next level = " + rowsToGo, new Vector2(400, 250), Color.Black);
            spriteBatch.DrawString(font, "Level = " + level, new Vector2(400, 300), Color.Black);

        }
        else if (gameState == GameState.Init)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(emptyCell, mousePos, Color.Red);
        }
        else
        {
            spriteBatch.Draw(endScreen, Vector2.Zero, Color.White);
            spriteBatch.DrawString(menuFont, "Score = " + score, new Vector2(180, 300), Color.Black);
            spriteBatch.DrawString(menuFont, "Level = " + level, new Vector2(680, 300), Color.Black);
        }
        spriteBatch.End();
    }

    public void Reset()
    {
        gameState = GameState.Init;
        grid.Clear();
        score = 0;
        rowsToGo = 20;
        level = 1;
        if (gameType == GameType.Normal)
            dropSpeed = 1000;
        else
            dropSpeed = 600;
    }

}
