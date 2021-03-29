using UnityEngine;

public class Ship : MonoBehaviour {

    /// Enemy ships are invisible at the start and will not be revealed until completely destroyed.
    public bool submersible;  // enemy ship - originally invisible under water

    [Range(1, 4)]
	public int size;		// we always set the size (occupied cells) of the ships via inspector

    [HideInInspector]
    public int row;
    [HideInInspector]
    public int col;  // col + capacity is the ship's horizontal line

    private SpriteRenderer sr;
    private int health;         //internal health

    void Start () {
        sr = GetComponent<SpriteRenderer>();
        health = size;

        if (submersible)
        {
            Color transparent = sr.color;
            transparent.a = 0f;     // invisible enemy
            sr.color = transparent;
        }
	}

    public void Hit()       // called by Tile
    {
       // decrease health, and make fully visible when dead
    }

}
