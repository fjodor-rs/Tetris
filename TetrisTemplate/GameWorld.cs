using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        Playing,
        GameOver
    }

    /// <summary>
    /// The random-number generator of the game.
    /// </summary>
    public static Random Random { get { return random; } }
    static Random random;

    /// <summary>
    /// The main font of the game.
    /// </summary>
    SpriteFont font;

    /// <summary>
    /// The current game state.
    /// </summary>
    GameState gameState;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    TetrisGrid grid;

    TetrisBlock tetrisBlock, drawBlock;
    int nextBlock, currentBlock, dropSpeed, previousTime;
    TetrisBlock.Block blockType;
    Color blockColor;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");

        grid = new TetrisGrid();
        ResetBlock();
        dropSpeed = 1000;
        previousTime = 0;
    }

    public void ResetBlock()
    {
        currentBlock = nextBlock;
        nextBlock = Random.Next(7);
        BlockIndex(currentBlock);
        tetrisBlock = new TetrisBlock(blockType, blockColor, grid);
        BlockIndex(nextBlock);
        drawBlock = new TetrisBlock(blockType, blockColor);
        drawBlock.Position = new Point(13, 5);
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
        }
    }

    public void MoveDown()
    {
        previousTime -= dropSpeed;
        tetrisBlock.Position += new Point(0, 1);
        if (tetrisBlock.BottomBounds())
        {
            tetrisBlock.Position += new Point(0, -1);
            tetrisBlock.BlockToGrid();
            ResetBlock();
        }
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {

		if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
		{
				tetrisBlock.Position -= new Point(1, 0);
            if (tetrisBlock.SideBounds())
                tetrisBlock.Position -= new Point(-1, 0);

        }

		if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
		{
				tetrisBlock.Position += new Point(1, 0);
            if (tetrisBlock.SideBounds())
                tetrisBlock.Position += new Point(-1, 0);
        }

		if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
		{
            MoveDown();
        }
        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
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

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
        {
            ResetBlock();
        }
    }

    public void Update(GameTime gameTime)
    {
        previousTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
        if (previousTime >= dropSpeed)
        {
            MoveDown();
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch);
        tetrisBlock.Draw(gameTime, spriteBatch);
        drawBlock.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public void Reset()
    {
    }

}
