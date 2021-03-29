using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Button[] topButtons;
    public GameObject tileTemplate;

    private static char turn = 'X';

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
    
    public void Drop(int column)
    {
        // see if this column has a vacancy
        int rowIx = -1;
        for (int i = 0; i < 6; i++)
            if (board[i, column] == ' ')
                rowIx = i;

        if (rowIx == -1)
            return;     // fully taken

        board[rowIx, column] = turn;       // reserve

        Vector2 startPos = topButtons[column].transform.position;

        GameObject square = (GameObject) Instantiate(tileTemplate, startPos, Quaternion.identity);
        Tile tile = square.GetComponent<Tile>();
        
        //print("Tile name: Tile " + rowIx + column);

        tile.Drop(rowIx, column, turn, startPos.y - rowIx * 1.93f - 1.75f);

        turn = turn == 'x' ? 'o' : 'x';
        CheckWin();     // a winning tile will be dropped, gives a nice effect
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

   

}
