using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class TetrisBlock
{
    public enum Block { I, J, L, S, Z, T, O , Rod, Cup, Mill };
    bool[,] shape, rotatedShape;
    Block block;
    Point position;
    Texture2D emptyCell;
    Color color;
    TetrisGrid grid;

    public Point Position { get { return position; } set { position = value; } }

    public TetrisBlock(Block b, Color c, TetrisGrid grid = null)
    {
        block = b;
        color = c;
        this.grid = grid;
        shape = new bool[4, 4];
        rotatedShape = new bool[4, 4];
        SetShape();
        position = new Point(3, -1);
    }

    private void SetShape()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        // 4x4 grid voor de blokken.
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                shape[i, j] = false;
                rotatedShape[i, j] = false;
            }
        }
        // Ieder mogelijk block voor de shape grid.
        switch (block)
        {			
            case Block.I:
                shape[1, 0] = true;
                shape[1, 1] = true;
                shape[1, 2] = true;
                shape[1, 3] = true;
                break;
            case Block.J:
                shape[1, 2] = true;
                shape[2, 2] = true;
                shape[2, 1] = true;
                shape[2, 0] = true;
                break;
            case Block.L:
                shape[1, 0] = true;
                shape[1, 1] = true;
                shape[1, 2] = true;
                shape[2, 2] = true;
                break;
            case Block.S:
                shape[3, 1] = true;
                shape[2, 1] = true;
                shape[2, 2] = true;
                shape[1, 2] = true;
                break;
            case Block.Z:
                shape[1, 1] = true;
                shape[2, 1] = true;
                shape[2, 2] = true;
                shape[3, 2] = true;
                break;
            case Block.T:
                shape[0, 2] = true;
                shape[1, 2] = true;
                shape[2, 2] = true;
                shape[1, 1] = true;
                break;
            case Block.O:
                shape[1, 1] = true;
                shape[1, 2] = true;
                shape[2, 1] = true;
                shape[2, 2] = true;
                break;
            case Block.Rod:
                shape[1, 0] = true;
                shape[1, 1] = true;
                shape[2, 1] = true;
                shape[2, 2] = true;
                shape[3, 2] = true;
                shape[3, 3] = true;
                break;
            case Block.Cup:
                shape[0, 2] = true;
                shape[0, 1] = true;
                shape[1, 2] = true;
                shape[2, 2] = true;
                shape[3, 1] = true;
                shape[3, 2] = true;
                break;
            case Block.Mill:
                shape[1, 1] = true;
                shape[1, 2] = true;
                shape[2, 1] = true;
                shape[2, 2] = true;
                shape[1, 0] = true;
                shape[3, 1] = true;
                shape[2, 3] = true;
                shape[0, 2] = true;
                break;
        }
    }

    // True is met de klok mee roteren, false is tegen de klok in roteren.
    public void Rotate(bool clockwise = true)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
				if (clockwise)
					rotatedShape[3 - y, x] = shape[x, y];
				else
					rotatedShape[y, 3 - x] = shape[x, y];
			}
		}

		for (int x = 0; x < 4; x++)
		{
			for (int y = 0; y < 4; y++)
			{
			   shape[x, y] = rotatedShape[x, y];
			}
		}
	}

    
	// Zorgt ervoor dat het blokje er niet aan de zijkant uit kan.
    public bool SideBounds()
	{
		for (int x = 0; x < 4; x++)
		{
			for (int y = 0; y < 4; y++)
			{
				if (shape[x, y])
				{
                    if (position.Y + y >= 0)
                    {
                        if (position.X + x >= 10 || position.X + x < 0)
                        return true;
                        else if (grid.BlockGrid[position.X + x, position.Y + y] != Color.White)
                        return true;
                    }
                }
			}
		}
		return false;
	}
	// Checkt voor collision met de onderkant en andere blokjes.
    public bool BottomBounds()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (shape[x, y])
                {
                    if (position.Y + y >= 20)
                        return true;
                    else if (position.X + x < 10 && position.X + x >= 0)
                    {
                        if (position.Y + y >= 0)
                            if (grid.BlockGrid[position.X + x, position.Y + y] != Color.White)
                            return true;
                    }
                }
            }
        }
        return false;
    }

	// Zet de posities van blokjes om naar positie op de color grid.
    public void BlockToGrid()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (shape[x, y])
                {
                    if (position.Y + y >= 0)
                        grid.BlockGrid[position.X + x, position.Y + y] = color;
                }
            }
        }
    }

	// Tekent de blokjes op de grid.
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
			{
				if (shape[x,y])
                {
                    spriteBatch.Draw(emptyCell, new Vector2((position.X + x) * emptyCell.Width, (position.Y + y) * emptyCell.Height), color);
                }
			}
        }
    }
}