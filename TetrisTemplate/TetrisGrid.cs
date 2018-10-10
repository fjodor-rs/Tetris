﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    ///grid-------------->?--------------------------------------------
    Color[,] blockGrid;
    public Color[,] BlockGrid { get { return blockGrid; } set { blockGrid = value; } }

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }
   
    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        position = Vector2.Zero;
        Clear();
        blockGrid = new Color[Width, Height];

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                blockGrid[i, j] = Color.White;
            }
        }
    }

    public void LineCheck()
    {
        bool full;
        for (int i = 19; i > 0; i--)
        {
            full = true;
            for (int j = 0; j < 9; j++)
            {
                if (blockGrid[j, i] == Color.White)
                {
                    full = false;
                    break;
                }
                if (full)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        blockGrid[j, i] = blockGrid[j, i - 1];
                    }
                }
            }
        }

    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                spriteBatch.Draw(emptyCell, new Vector2(i * emptyCell.Width, j * emptyCell.Height), blockGrid[i, j]);
            }
        }
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
	/// 
    public void Clear()
    {
    }
}

