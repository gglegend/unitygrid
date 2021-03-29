using UnityEngine;

// actual heavy lifting of placing ships on board. Plain class
public class BoardManager 
{
    // fleet is ordered largest to smallest, for more optimal placement
    public void PlaceFleet(GameObject[] fleet, Tile[,] board)
    {
        // place ships, from largest to smallest, in the open spaces
        // alternative algo would be to find an open space, 
        //    and place largest ship that fits in it
        for (int ix = 0; ix < fleet.Length; ix++)
        {
            GameObject ship = fleet[ix];
            int len = ship.GetComponent<Ship>().size;

            bool placed = false;
            while (placed == false)     // possible infinite loop? not likely
            {
                // get a random open location, try to fit the largest ship in there
                int row = Random.Range(0, 8);
                //int col = Random.Range(0, 8 - len);  // makes it more clustered in the middle
                int col = Random.Range(0, 8);

                int capacity = CountOpenSpaces(board, row, col);
                if (capacity >= len)
                {
                    Place(board, row, col, ship, len);
                    placed = true;
                }
            }
        }
    }

    // ships anchor left to right
    int CountOpenSpaces(Tile [,] board, int row, int col)
    {
        int count = 0;
        for (int i = col; i < 8; i++)
        {
            if (board[row, i].ship == null && board[row, i].gap == false)
                count++;
            else
                break;  // run into occupied space
        }
        return count;
    }

    void Place(Tile [,] board, int row, int startCol, GameObject ship, int length)
    {
        // center the ship in the middle between start & end tiles
        GameObject startTile = board[row, startCol].gameObject;
        GameObject endTile = board[row, startCol + length - 1].gameObject;
        Vector2 midPoint = (startTile.transform.position + endTile.transform.position) / 2f;
        ship.transform.position = midPoint;

        // mark the tiles as occupied by a ship
        for (int i = startCol; i < startCol + length; i++)
            board[row, i].ship = ship;

        // mark the adjacent tiles as required gaps between ships (length-wise)
        if (startCol > 0)
            board[row, startCol - 1].gap = true;  // adjacent space taken
        if (startCol + length < 8)
            board[row, startCol + length].gap = true;
    }
}
