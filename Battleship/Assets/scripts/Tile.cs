using UnityEngine;

// fire at a given target
public class Tile : MonoBehaviour {

    public Sprite hitImg;
    public Sprite missImg;
    public bool interactive;        // is clickable or not (AI-controlled)
    private static Battery battery;

    [HideInInspector]
    public GameObject ship;        // null if not occupied

    [HideInInspector]
    public int x, y;

    private SpriteRenderer sr;

    public bool gap;        // adjacent to some ship, length-wise

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (battery == null)
        {
            GameObject bat = GameObject.Find("battery");
            battery = bat.GetComponent<Battery>();
        }
    }

    void OnMouseUp () {
        // check if it's our turn, if so:
        //   call battery.FireAt(this)
        
    }

    // called when ball collides with the tile
    public void Mark()
    {
        // set the hit or miss sprite, depending if the ship is placed on this tile
        // if ship is on the tile, notify the ship that it's been hit
        
    }
    
}
