using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour { 

    public Sprite imgX, imgY;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private float bottom = 0f;
    private bool started = false;

    void Awake()
    {
        // these must be done in Awake - because Drop() function often happens
        // before Start() completes, and components aren't initialized yet
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    public void Drop(int x, int y, char symbol, float endY)
    {
        gameObject.name = "Tile" + x + y;  // to highlight if a winning tile
        bottom = endY;

        // TODO: set gravity scale
        // set the proper sprite

        started = true;
    }

    void FixedUpdate()
    {
        if (started == false)
            return;     // not moving yet

        // if reached bottom,
        // set body gravity scale & velocity to 0
       
    }

}
