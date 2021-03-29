using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class TetrisView : MonoBehaviour
{
    public GameObject Square;
    public Text Score;
    public Text GameOverText;
    //public Color I, J, L, O, S, T, Z;  // <- TODO

    /// xSpan and ySpan are the x and y sized of each square in the grid. 
    public float XSpan = 1f, YSpan = 1f;
    public float TopY = 0.5f;       // Y-offset, to alogn things verrically
    public int rows = 20; // 10X20 is the default tetris size
    public int cols = 10;
    public float tickTime = 1f;

    private float timeSpan; // this is the time span of one time frame in the tetris game

    /// the tetris game logic 
    Tetris TetrisGame; 

    /// this array holds all the unity objects of the grid
    GameObject[,] mGridItems;
  
    public void Start()
    {
        TetrisGame = new Tetris(cols, rows);
        timeSpan = tickTime;
        StartGame();
    }

    /// starts a new tetris game
    public void StartGame()
    {
        DeleteGrid(); // delete the old grid
        mGridItems = new GameObject[TetrisGame.GridSizeX, TetrisGame.GridSizeY]; // create a new one

        float halfX = XSpan * TetrisGame.GridSizeX * 0.5f;
        float halfY = YSpan * TetrisGame.GridSizeY * 0.5f; // this are used in order to center the grid around 0

        for (int i = 0; i < TetrisGame.GridSizeX; i++) // create an object for each element in the grid
            for (int j = 0; j < TetrisGame.GridSizeY; j++)
            {
                mGridItems[i, j] = GameObject.Instantiate(Square); 
                mGridItems[i, j].transform.position = new Vector3(XSpan * i - halfX, YSpan * j - halfY + TopY); // set it's position based on the i and j params
            }
        GameOverText.gameObject.SetActive(false);
        TetrisGame.StartGame(); // start the game logic
    }

    public void UpdateGame()
    {
        for (int i = 0; i < TetrisGame.GridSizeX; i++)
            for (int j = 0; j < TetrisGame.GridSizeY; j++)
            {
                if (TetrisGame.Grid[i, j] == 1) // for each item in tetris grid
                    mGridItems[i, j].gameObject.SetActive(true); // we either make the game object active 
                else
                    mGridItems[i, j].gameObject.SetActive(false); // or inactive
            }
   
    }

    /// deletes the current grid objects in order to make a new grid
    void DeleteGrid()
    {
        if (mGridItems == null)
            return;

        for (int i = 0; i < TetrisGame.GridSizeX; i++)
            for (int j = 0; j < TetrisGame.GridSizeY; j++) // iterate all the objects
                GameObject.Destroy(mGridItems[i, j]);  // destroy them
        mGridItems = null;
    }

    public void GameOver()
    {
        GameOverText.gameObject.SetActive(true);
    }
   

    void Update()
    {
        if (TetrisGame.isGameOver)
        {
            GameOver();
            return;
        }

        bool gameChanged = false; // we only update the grid if it is chagned to avoid unessary work

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TetrisGame.TryAdvanceCurrentBlock(-1, 0); // on left key press , advance by -1 on the x axis
            gameChanged = true;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TetrisGame.TryAdvanceCurrentBlock(1, 0); // on right key press , advance by 1 on the x axis
            gameChanged = true;
        }

        if (Input.GetKeyDown(KeyCode.RightControl)) // on control press
        {
            TetrisGame.TryRotateCurrentBlock(); // try rotating the block
            gameChanged = true;
        }

        timeSpan -= Time.deltaTime; // remove the elasped time from the timer
        if (timeSpan <= 0f || Input.GetKey(KeyCode.DownArrow)) // we check either if the time frame has elsaped or if the down arrow is pressed (down arrow speed the time)
        {
            timeSpan = tickTime; // reset the time frame timer
            TetrisGame.MakeTurn(); // advance the game by one time frame
            gameChanged = true;
        }

        if(gameChanged)
            UpdateGame(); // update the view grid

        Score.text = "Score : " + TetrisGame.Score; // update the score
    }
}
