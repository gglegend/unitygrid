using System;

class Matrix
{
    
    public static int CountRow(char[,] board, int rowId)
    {
        int count = 0;

        // Count ocurrences of 'X' in a given row of the board
        return count;
    }

    ///////////////////////////////////////////////////////////////////
    /////////////////// count the diagonals:  /////////////////////////
    ///////////////////////////////////////////////////////////////////
    public static int CountMainDiagonal(char[,] board)
    {
        // Implement after everything else is done
        return 0;
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
