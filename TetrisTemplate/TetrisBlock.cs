using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class TetrisBlock
{
    public enum Block {I, J, L, S, Z, T, O};
    bool[,] shape;
    Block block;

    public TetrisBlock(Block b)
    {
        block = b;
        shape = new bool[4, 4];
        SetShape();
        
    }

    private void SetShape()
    {
        // 4x4 grid voor de blokken
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                shape[i, j] = false;
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
                shape[2, 1] = true;
                shape[2, 2] = true;
                shape[2, 3] = true;
                shape[1, 3] = true;
                break;
            case Block.L:
                shape[1, 1] = true;
                shape[1, 2] = true;
                shape[1, 3] = true;
                shape[2, 3] = true;
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

}

