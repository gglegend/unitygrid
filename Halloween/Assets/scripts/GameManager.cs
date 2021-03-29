using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Button[] topButtons;

    private static char turn = 'x';

    // keep color private as it is static, together with everything else
    public Color winningColor = Color.white;

    private static char[,] board = {        // 6x6
        { ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', },
        { ' ', ' ', ' ', ' ', ' ', ' ', },
    };   // empty at start

    
    public void Set(int row, int col, char c)
    {
        board[row, col] = c;
        CheckWin();
    }

    //////////////////////// highlight the winning sprites: /////////////////////////
    void Highlight(GameObject go, Color c)
    {
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.color = c;
    }

    void Highlight(string [] sequence) // highlight winning sequence of elements
    {
        foreach (string winningIndex in sequence)
        {
            GameObject winningTile = GameObject.Find("Tile" + winningIndex);
            // print("winner: " + winningTile);
            Highlight(winningTile, winningColor);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////// board cutting and slicing  ////////////////////////////////////
    ////////////// uses Matrix.cs from other projects verbatim, with a tiny adjustment ///////////
    public char[,] Subset(char[,] matrix, int rowShift, int colShift)
    {
        char[,] reduced = new char[4, 4];
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                reduced[i, j] = matrix[i + rowShift, j + colShift];

        return reduced;
    }

    void ChecWinningOrthogonals()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                char[,] fourByFour = Subset(board, row, col);
                CountRows(fourByFour, row, col, 'x');
                CountRows(fourByFour, row, col, 'o');
                CountColumns(fourByFour, row, col, 'x');
                CountColumns(fourByFour, row, col, 'o');
            }
        }
    }

    void HighlightRow(int startRow, int startCol)
    {
        string[] winners = new string[] { startRow +  "" + startCol,  startRow + ""+ (startCol+1),
                    startRow +""+ (startCol + 2), startRow + ""+ (startCol + 3) };
        Highlight(winners);
    }

    void HighlightColumn(int startRow, int startCol)
    {
        string[] winners = new string[] { startRow +  "" + startCol,  startRow+1 + ""+ startCol,
                    startRow +2+""+ startCol, startRow + 3 + ""+ startCol };
        Highlight(winners);
    }

    void CountRows(char[,] fourByFour, int startRow, int startCol, char c)
    {
        for (int x = 0; x < 4; x++)
        {
            int lineCount = 0; // Matrix.CountHoriz(fourByFour, x, c);        // check first row
            if (lineCount == 4)     // re-adjust indices from 4x4 to 6x6:
                HighlightRow(x + startRow, startCol);
        }
    }

    void CountColumns(char[,] fourByFour, int startRow, int startCol, char c)
    {
        for (int y = 0; y < 4; y++)
        {
            int lineCount = 0; // Matrix.CountVert(fourByFour, y, c);        // check first row
            if (lineCount == 4)     // re-adjust indices from 4x4 to 6x6:
                HighlightColumn(startRow, y + startCol);
        }
    }

    void CheckMainDiag(char[,] fourByFour, int startRow, int startCol, char c)
    {
        int cnt = 0; // Matrix.CountMainDiag(fourByFour, c, 4);
        if (cnt == 4)
        {
            // main diagonals go: (0,0), (1,1), (2,2), (3,3)
            Highlight(new string[] { startRow + "" + startCol, startRow + 1 +""+ (startCol+1),
                                   startRow + 2 + ""+ (startCol+2), startRow +3 + ""+ (startCol+3) });
        }
    }

    void CheckAuxDiag(char[,] fourByFour, int startRow, int startCol, char c)
    {
        int cnt = 0; //  Matrix.CountSecondDiag(fourByFour, c, 4);
        if (cnt == 4)
        {
            // aux diagonals go: (0,3), (1,2), (2,1), (3,0)
            Highlight(new string[] { startRow + "" + (startCol+3), startRow + 1 +""+ (startCol+2),
                                   startRow + 2 + ""+ (startCol+1), startRow +3 + ""+ startCol }); ;
        }
    }

    void CheckWinningDiag()
    {
        for (int row = 0; row < 3; row++)   // shift by 0, 1 and 2 positions
            for (int col = 0; col < 3; col++)
            {
                char[,] fourByFour = Subset(board, row, col);
                CheckMainDiag(fourByFour, row, col, 'x');
                CheckMainDiag(fourByFour, row, col, 'o');

                CheckAuxDiag(fourByFour, row, col, 'x');
                CheckAuxDiag(fourByFour, row, col, 'o');
            }
    }

    void CheckWin()
    {
        // we will have a nice effect: if more than one row, column or diagonal are won,
        // they all will be highlighted
        ChecWinningOrthogonals();   // rows and columns
        CheckWinningDiag();
    }

}
