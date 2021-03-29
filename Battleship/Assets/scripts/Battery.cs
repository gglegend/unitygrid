using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// an artillery Battery that controls cannon fire
// we have one battery shooting at both fleet
public class Battery : MonoBehaviour {

    public GameObject [] gunz;
    public GameObject cannonball;
    
    public static string turn = "player";
    Cannon[] cannons;
    private List<Tile> playerTiles = null;  // for AI aiming

    int shots = 0;  // index of the next cannon to shoot

    // as AI is dumb, we'll give it 6 shots vs player's 4, per each turn
    int AI_SHOTS_PER_TURN = 6;

    void Start()
    {       
        // locate a set player tiles for AI targeting
        GameObject playerFleet = GameObject.Find("PlayerFleet");
        playerTiles = playerFleet.GetComponentInChildren<Fleet>().getTiles();
      
        cannons = new Cannon[gunz.Length];
        for (int i = 0; i < gunz.Length; i++)
            cannons[i] = gunz[i].GetComponent<Cannon>();
    }

    // this is called in two instances:
    //    Player clicked on a tile
    //    AI requested shot on a target
	public void FireAt (Tile target) {

        // Shoot() the next cannon, using 'shots' as an index
        // ...

        // give turn to AI when all cannons have fired
        if (shots >= cannons.Length) {
            shots = 0;
            if (turn == "player")
                turn = "ai";            
        }
    }
   
    // should shoot up targets in a loop, and later as a Corutine
    void AIShoot()
    {    
        List <Tile> targets = PickRandomTargets();
        
        FireAt(targets[0]);        
        
        shots = 0;
        turn = "player";
    }

    private List<Tile> PickRandomTargets()
    {
        // TODO: pick a few random tiles off the list
        return playerTiles;
    }

    private void FixedUpdate()
    {
        if(turn == "ai")
        {            
            AIShoot();  // TODO: change to a Coroutine
            shots = 0;
            turn = "none";      // so that neither player no ai can get to it while it's in mid-execution
        }
    }
}