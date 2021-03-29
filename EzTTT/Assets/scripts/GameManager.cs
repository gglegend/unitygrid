using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// a variation of BoardManager from the VS Console exercise:
public class GameManager : MonoBehaviour {

    // keep color private as it is static, together with everything else
    public Color winningColor = Color.white;

    private static char[,] board = {     
        { ' ', ' ', ' ' },
        { ' ', ' ', ' ' },
        { ' ', ' ', ' ' },
    };   // empty at start
   
    public void Set(int row, int col, char c)
    {
        board[row, col] = c;
        CheckWin();
    }

    //////////////////////////////////////////////////////////////////////////
    /////// select winning gameobjects and paint them in winning color  //////
    //////////////////////////////////////////////////////////////////////////
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
            Highlight(winningTile, winningColor);
        }
    }

    /////////////////////////////////////////////////////////////////////////
    //////////  use code from BoardManager in 2DArray prject  ///////////////
    //////////////// to provide the needed functionality ////////////////////
    /////////////////////////////////////////////////////////////////////////

    void CheckWin() {

        int num = 0;

        for (int row = 0; row < 3; row++)
        {
            int numX = Matrix.CountRow(board, row, 'X');  // count X in top row
            int numY = Matrix.CountRow(board, row, 'O');
            if (numX == 3 || numY == 3)
            {
                string[] winners = { row + "0", row + "1", row + "2" };  // <- if first row wins, we highlight its top three tiles:
                Highlight(winners);
            }
        }

        
        for (int column = 0; column < 3; column++)
        {
            int numX = Matrix.CountColumn(board, column, 'X');  // count X in top row
            int numY = Matrix.CountColumn(board, column, 'O');
            if (numX == 3 || numY == 3)
            {
                string[] winners = {"0" + column, "1" + column , "2" + column};  // <- if first row wins, we highlight its top three tiles:
                Highlight(winners);
            }
        }


        num = Matrix.CountMainDiagonal(board);
            if (num == 3)
            {
                string[] winners = { "00", "11", "22" };  // <- if first row wins, we highlight its top three tiles:
                Highlight(winners);
                return;
            }
        
        num = Matrix.CountSecondDiagonal(board);
        if (num == 3)
        {
            string[] winners = { "02", "11", "20" };  // <- if first row wins, we highlight its top three tiles:
            Highlight(winners);
            return;
        }


        // check rows 
        // check columns
        // check main and secondary diagonals



    }

}
