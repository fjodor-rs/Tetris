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

    TetrisBlock tetrisBlock;
    TetrisBlock.Block blockType;
    Color blockColor;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");

        grid = new TetrisGrid();
        CreateBlock();
    }

    public void CreateBlock()
    {
        switch (Random.Next(7))
        {
            case 0:
                blockType = TetrisBlock.Block.I;
                blockColor = Color.Aqua;
                break;
            case 1:
                blockType = TetrisBlock.Block.J;
                blockColor = Color.YellowGreen;
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

        tetrisBlock = new TetrisBlock(blockType, blockColor);

    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
        {
            tetrisBlock.Position -= new Point(1, 0);
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
        {
            tetrisBlock.Position += new Point(1, 0);
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
        {
            tetrisBlock.Position += new Point(0, 1);
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
        {
            tetrisBlock.Rotate();
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
        {
            CreateBlock();
        }
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch);
        tetrisBlock.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public void Reset()
    {
    }

}
