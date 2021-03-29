using System.Collections.Generic;
using UnityEngine;

// one Fleet for the player, and one for the AI board (submarines)
public class Fleet : MonoBehaviour {

    public GameObject single, twin, triple, flagman;
    private Tile[,] board;
    private List<Tile> tileList = new List<Tile>();

    void Awake () {
        BoardManager admiral = new BoardManager();
        BuildTiles();
        GameObject [] fleet = BuildFleet();
        admiral.PlaceFleet(fleet, board);
	}

    public List <Tile> getTiles()
    {
        return tileList;
    }

    void BuildTiles()
    {
        board = new Tile[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject go = FindObject("Tile" + i + j);
                board[i, j] = go.GetComponent<Tile>();
                tileList.Add(go.GetComponent<Tile>());
            }
        }
    }
  
    GameObject [] BuildFleet()
    {        
        GameObject[] fleet = new GameObject[10];
        // instantiate ships, from largest to smallest

        return fleet;
    }

    private GameObject FindObject(string name)
    {
        Transform[] ts = gameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == name) return t.gameObject;
        return null;
    }  
   
}       
