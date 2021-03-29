using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

    public static bool[,] board = new bool[3, 3];   // one for all    

    private int x;
    private int y;
    
     // after Tile is created by manager, it's given x and y coord
     // place it on the screen
    public void Place(int row, int col)
    {
        x = row;
        y = col;
        // on-screen screen y cood increases toward's screen bottom
        // so we invert the y-position on-screen
        transform.position = new Vector2(x, 2-y); 
        board[x, y] = true;     // taken
    }

    // mouse clicked, check if any neighbor slot is available, and move there
    // row 0 is at the bottom, row 3 is at top, so y++ is towards bottom of the screen
    void OnMouseDown() {

        //        print("my coords: " + x + ", " + y);

        // check board in left direction
        // if space is available:
        // move image on screen to the left
        // mark left space on board as taken
        // mark my current space on board as vacant
        // update my own x-coordinate as : x--
        // 
        // do the same for other three directions
      
       
    }

}
