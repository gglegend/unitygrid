using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public Sprite imgX, imgO, empty;
    private static char turn = 'X';
    

    private SpriteRenderer sr;
    private int row, col;        // used only to determine wins, unused otherwise
    GameManager gameKeeper;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameKeeper = FindObjectOfType<GameManager>();
        // Tile12-> row1, col2
        char r = gameObject.name[4];
        char c = gameObject.name[5];

        row = int.Parse("" + r);
        col = int.Parse("" + c);
    }

    void OnMouseUpAsButton()
    {

        // check if space is available
        // if so,
        // set to X or O

        if (sr.sprite != empty)
            return;

        gameKeeper.Set(row, col, turn); // sets piece on main board, and checks for win

        if (turn == 'X')
        {
            turn = 'O';
            sr.sprite = imgX;
        }
            

       else 
        {
            turn = 'X';
            sr.sprite = imgO;
        }

	}
    
}
