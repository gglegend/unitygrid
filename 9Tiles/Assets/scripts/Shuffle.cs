using System.Collections.Generic;
using UnityEngine;

// layout tiles in random order at the start of the game
public class Shuffle : MonoBehaviour {

    // x are columns (left-right), y are rows (top-bottom)
    private List<Tile> tiles = new List<Tile>();

    void Start()
    {
        Tile[] tz = GetComponentsInChildren<Tile>();

        List<Tile> pool = new List<Tile>();
        pool.AddRange(tz);
        int size = pool.Count;

        // get random tilez from the pool 
        for (int i=0; i< size; i++)
        {
            int r = Random.Range(0, pool.Count);
            Tile randomTile = pool[r];
            pool.RemoveAt(r);
            tiles.Add(randomTile);
        }

        PlaceOnBoard();
    }

    public void PlaceOnBoard() {

        int ix = 0;

        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++) 
            {
                // we have 9 cells and only 8 tiles
                if (x == 2 && y == 2)
                    return;                 // last tile is empty

                Tile t = tiles[ix++];
                t.Place(x, y);          // invert rows and cols
            }
    }    
}

