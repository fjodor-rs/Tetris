using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class TetrisBlock
{
    public enum Block { I, J, L, S, Z, T, O };
    bool[,] shape, rotatedShape;
    Block block;
    Vector2 position;
    Texture2D emptyCell;
    Color color;

    public Vector2 Position { get { return position; } set { position = value; } }

    public TetrisBlock(Block b, Color c)
    {
        block = b;
        color = c;
        shape = new bool[4, 4];
        rotatedShape = new bool[4, 4];
        SetShape();
        position = new Vector2(3, 0);
        
    }

    private void SetShape()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        // 4x4 grid voor de blokken
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                shape[i, j] = false;
                rotatedShape[i, j] = false;
            }
        }

        //ieder apart block
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
        }

    }

    public void Rotate() //clockwise atm
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                rotatedShape[(3 - y), x] = shape[x, y];
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

