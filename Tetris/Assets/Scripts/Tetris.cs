using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris
{
    /// the tetris grid , 1 is full 0 is empty
    public int[,] Grid;
    /// an array of all the tetris block available (where each block is a 2D int array from TetrisShapes.cs)
    int[][,] Blocks;

    public int Score = 0;
    public bool isGameOver = true;

    public readonly int GridSizeX = 2; // 10X20 is the default tetris size
    public readonly int GridSizeY = 2;

    /// the current x and y positions of the current block
    int blockX, blockY;
    /// the currently falling block (or null if there isn't any)
    int[,] CurrentBlock;

    public Tetris(int cols, int rows) 
    {
        GridSizeX = cols;
        GridSizeY = rows;
    }

    /// starts a new tetris game.
    public void StartGame()
    {
        isGameOver = false;
        Score = 0;

        // the grid y size is allocated with additional rows.
        // This make it so that for loops don't cause an indexOutOfBounds exception, this makes the code simpler
        Grid = new int[GridSizeX, GridSizeY + TetrisShapes.ShapeSize];  

        CurrentBlock = null;  
        // all available shapes for the game:
        Blocks = new int[][,] {  TetrisShapes.IBlock, TetrisShapes.JBlock, TetrisShapes.LBlock,
            TetrisShapes.OBlock, TetrisShapes.SBlock, TetrisShapes.ZBlock, TetrisShapes.TBlock };
    }

    /// return true if the x and y positions are contained in the Grid array.
    /// This basically checks that the x and y are both non negative and smaller then the Grid Array size
    bool IsPositionInGrid(int x,int y)
    {
        if (x < 0 || y < 0) // if one is negative then this position is out of the Grid
            return false;
        if (x >= GridSizeX) // check that the x is smaller then GridSizeX. Otherwise return false
            return false;
        if (y >= GridSizeY + TetrisShapes.ShapeSize) // In StartGame() the Grid is allocated with additional rows , so we check that the y position is within that range
            return false;
        return true; // if non of the above conditions is met , it means the x,y position is inside the grid
    }

    /// checks if a block is contained within the grid.
    bool IsBlockInGridBounds(int x,int y, int[,] block)
    {
        for (int i = 0; i < TetrisShapes.ShapeSize; i++)
        {
            for (int j = 0; j < TetrisShapes.ShapeSize; j++) // iterate all the positions in block array
            {
                if (block[i, j] == 1) // for each part of the block , we must check if it's inside the grid. if the current square in the shape is empty , there is no need for additional checks
                {
                    if (IsPositionInGrid(x + i, y + j) == false) // x+i and y+j. are the position of the square on the grid itself. x,y is the position of the shape and i,j is the position within the shape.  
                        return false;// if it's out of the grid, return false;
                }
            }
        }
        return true; // we have check all squares in the block and couldn't find a position that is out of the grid. We can assert that the entire block is within the grid
    }

    /// checks if the a block collides with previous blocks on the grid. 
    bool BlockCollidesWithGrid(int x, int y, int[,] block)
    {
        for (int i = 0; i < TetrisShapes.ShapeSize; i++)
        {
            for (int j = 0; j < TetrisShapes.ShapeSize; j++) // iterate all the positions in block array
            {
                // if the block position is x,y and the position within the block is i,j
                // then x+i,y+j is the position of the current square on the grid.
                if (block[i, j] == 1 && Grid[x + i, y + j] == 1) // we check if that square is already full (equals 1)
                    return true; // if it is then the block collides with the grid.
            }
        }
        return false; // we couldn't find any collition , so the block does not collide
    }

    /// write a block into the grid at the specified position
    void WriteBlockToGrid(int x ,int y,int[,] block)
    {
        for (int i = 0; i < TetrisShapes.ShapeSize; i++)
        {
            for (int j = 0; j < TetrisShapes.ShapeSize; j++) // iterate all the positions in block array
            {
                // if the block position is x,y and the position within the block is i,j
                // then x+i,y+j is the position of the current square on the grid.
                if (block[i,j] == 1)  // if this is a full square in the block (empty squares are irrelevant)
                    Grid[x + i, y + j] = 1; // write it to the grid
            }
        }
    }

    /// after calling WriteBlockToGrid , we can call DeleteBlockFromGrid to delete all the written squares. 
    /// this can be used to animated the motion of blocks as they are written and delted and then rewritten in a new position
    void DeleteBlockFromGrid(int x, int y,int[,] block)
    {
        for (int i = 0; i < TetrisShapes.ShapeSize; i++)
        {
            for (int j = 0; j < TetrisShapes.ShapeSize; j++) // iterate all the positions in block array
            {
                // if the block position is x,y and the position within the block is i,j
                // then x+i,y+j is the position of the current square on the grid.
                if (block[i, j] == 1) // if this is a full square in the block (empty squares are irrelevant)
                    Grid[x + i, y + j] = 0; // delete it from the grid
            }
        }
    }
    
    /// checks is a row in the grid is full. Full rows give score
    bool IsRowFull(int y)
    {
        for (int i = 0; i < GridSizeX; i++)
            if (Grid[i, y] == 0) // if we have even 1 empty grid position in the row
                return false;       // then it is not full
        return true; // we have gone through all the row and couldn't find an empty position. So the row is full
    }

    /// clears a row in the grid
    void ClearRow(int y)
    {
        for (int i = 0; i < GridSizeX; i++) // go through all items in the row
            Grid[i,y] = 0; // and set them to 0
    }

    /// <summary>
    /// copys a row from "fromY" to "toY"
    /// </summary>
    /// <param name="fromY"></param>
    /// <param name="toY"></param>
    void CopyRow(int fromY,int toY)
    {
        for (int i = 0; i < GridSizeX; i++) // go through all indices in GridSizeX
            Grid[i, toY] = Grid[i, fromY]; // copy each from "fromY" to "toY"
    }

    /// Deletes a row and moves all the rows above it one row down
    /// for example with this grid:
    /// 
    /// XXXX
    /// X X
    ///  X X
    /// XXX
    ///
    /// removing row number 2:
    /// 
    /// XXXX
    /// X X
    /// 
    /// XXX
    /// 
    /// and then moving the rows above it:
    /// 
    /// XXXX
    /// X X
    /// XXX
    void DeleteRow(int y)
    {
        int lastRow = GridSizeY - 1;
        for (int i=y; i< lastRow; i++) // in tetris , when a row is deleted all the rows above it move down. So we have to copy each row from the row above it. We never get to the last row (because we are using smaller then opeartor)
            CopyRow(i+1,i);
        // when we get to the last row in the grid, there is no row above it , so simply clear it from any blocks
        ClearRow(lastRow);
    }

    /// selects a new random block and sets it's position to the top of the grid
    void CreateRandomBlock()
    {
        CurrentBlock = Blocks[Random.Range(0, Blocks.Length)]; // select a random block
        blockX = GridSizeX / 2; // the center of the row is the size of row divided by 2
        blockY = GridSizeY - 1; // the last row is the top of the grid
    }

    /// trys advancing the current block , returns true on success and false on failure
    /// a block is moved by deleting it from it's old position and then writing it in it's new position. If the new position is occupied , the block is rewirtten in the old position and false is returned
    public bool TryAdvanceCurrentBlock(int x,int y)
    {
        if (CurrentBlock == null) // if there is no block
            return true; // we do nothing;
        bool AdvancedSuccessfully = true;

        DeleteBlockFromGrid(blockX, blockY, CurrentBlock); // delete the block from where it is now.

        blockX += x; // advance the block position
        blockY += y;

        //check if the new position can hold the new block (if it's within the grid and does not collide other blocks)
        if (IsBlockInGridBounds(blockX, blockY, CurrentBlock) == false || BlockCollidesWithGrid(blockX, blockY, CurrentBlock)) // if the block is out of the grid bounds, or collides with the current grid
        {
            // the new position can't hold the block
            blockX -= x; // move it back to it's position
            blockY -= y;

            AdvancedSuccessfully = false; // we could not advance
        }

        //blockX and blockY now hold the new position , or the old position if we could not advance
        WriteBlockToGrid(blockX, blockY, CurrentBlock); // regardless of if the blocked moved or not, we must rewrite it. It will be either be written in the new position or in the old position (if we could not advance)
        return AdvancedSuccessfully;
    }

    /// checks for full row and removes them , increasing the score by 1 for each row
    void RemoveFullRows()
    {
        for (int i = 0; i < GridSizeY; i++) // check each row to see if it's full
        {
            if (IsRowFull(i))
            {
                Score += 1; // we found a full row, delete it and increase the score
                DeleteRow(i); // delete the full row
                i--; // if the row is deleted it means the row above it is in position i now , this row may be full as well. so we must decrease i to check this row again
            }
        }
    }

    /// rotates a two dimentional array and returns the resulting two dimentional array
    int[,] RotateBlock(int[,] block)
    {
        int[,] newBlock = new int[TetrisShapes.ShapeSize, TetrisShapes.ShapeSize]; // create a new array to hold the rotated block
        for (int i = 0; i < TetrisShapes.ShapeSize; i++)
        {
            for (int j = 0; j < TetrisShapes.ShapeSize; j++) // iterate the block array
            {
                newBlock[i, j] = CurrentBlock[j, TetrisShapes.ShapeSize - 1 - i ]; // an object is rotated by 90 degrees by (switching it's x and y components and inverting it's y component). It's the easiest to understand how when you draw it on paper , try it
            }
        }
        return newBlock; // return the rotated block
    }

    /// try rotating the current block, if possible 
    public void TryRotateCurrentBlock()
    {
        if (CurrentBlock == null) // if there is no block
            return; // we do nothing;

        int[,] rotated = RotateBlock(CurrentBlock); // create a rotated copy of the block

        DeleteBlockFromGrid(blockX, blockY, CurrentBlock); // delete the current block from it's current position

        if (IsBlockInGridBounds(blockX, blockY, rotated) && BlockCollidesWithGrid(blockX, blockY, rotated) == false) // if after rotation the block is not colliding with the grid and is not out of the grid bounds
            CurrentBlock = rotated; // set the current block to the rotated block

        //rewrite the block (it can be either the rotated version or not , depending on the condition above)
        WriteBlockToGrid(blockX, blockY,CurrentBlock); // write the new block to the same position
    }

    /// the method is responsible for the action of one time frame in the tetris game
    public void MakeTurn()
    {
        if (isGameOver)
            return;
        if (CurrentBlock == null) // if there is no current block
        {
            CreateRandomBlock(); // create a new block
            if(BlockCollidesWithGrid(blockX, blockY, CurrentBlock)) // if the block we have created is colliding with the grid , then there is no more room for new blocks
            {
                isGameOver = true; // the game is lost
            }
        }
        else // if we have a block we advance it
        {
            if (TryAdvanceCurrentBlock(0, -1) == false) // try advancing the block on the y axis (-1) to make the block fall down by 1 square
            {
                // if TryAdvanceCurrentBlock returned false,  the block could not fall down any more
                if (IsBlockInGridBounds(blockX, blockY, CurrentBlock) == false) // if the block is out of the grid bounds after we could not advance it. It means the block it touching the top of the grid and the game is lost
                {
                    isGameOver = true;
                }
                else
                    RemoveFullRows(); // after the block is done falling , we must check if there are full rows
                CurrentBlock = null; // after the block is done falling , we set it to null to indicate that it is no longer controlled by the user. A new block should be created 
            }
        }
    }

}
