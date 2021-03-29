using System;

class Matrix
{
    
    public static int CountRow(char[,] board, int rowId, char letter)
    {
        int count = 0;

        if (board[rowId,0] == letter)
            count++;

        if (board[rowId, 1] == letter)
            count++;

        if (board[rowId, 2] == letter)
            count++;

        // Count ocurrences of 'X' in a given row of the board
        return count;
    }
  

    ///////////////////////////////////////////////////////////////////
    /////////////////// count the diagonals:  /////////////////////////
    ///////////////////////////////////////////////////////////////////
    public static int CountMainDiagonal(char[,] board)
    {
        // Implement after everything else is done

        int count = 0;

        if (board[0, 0] == 'X')
            count++;

        if (board[1, 1] == 'X')
            count++;

        if (board[2, 2] == 'X')
            count++;

        return count;
    }
    public static int CountColumn(char[,] board, int rowId, char letter)
    {
        int count = 0;

        //   char[] word = { 'x', 'o', 'a', 'x' };

        if (board[0, rowId] == letter)
            count++;

        if (board[1,rowId] == letter)
            count++;

        if (board[2,rowId] == letter)
            count++;

        // Count ocurrences of 'X' in a given row of the board
        return count;
    }
    // fully functional:
    public static int CountSecondDiagonal(char[,] board, char c='X', int size = 3)
    {
        int count = 0;
        int row = 0, column = size - 1;
        for ( ; row < size; row++, column--)
        {
            if (board[row, column] == c)
            {
                count++;
            }
        }
        return count;

  
        
    }
}
