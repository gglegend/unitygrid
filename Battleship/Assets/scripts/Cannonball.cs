using UnityEngine;

// let our cannonball fly to destination
public class Cannonball : MonoBehaviour {

    Vector3 startPoint;
	Tile target;	

	public AudioClip shootSfx;
    float speed = 50f;

    public void Launch(Vector3 cannonPos, Tile tgt)
    {
        startPoint = cannonPos;
        target = tgt;

        // play the sound
        

        // compute direction to target via vector subtraciton
        // launch the ball, using physics engine
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if we're colliding with our target, Mark() the tile, and destroy the ball
        
    }
 
}
