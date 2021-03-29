using UnityEngine;

// our center piece - not easily clickable
// so we let the 'U' button select it
public class Center : Block
{

    public static Transform ORIGIN;

    private void Start()
    {
        ORIGIN = transform;
        rend = GetComponent<Renderer>();
    }

    // Center cube is not easily accessible, so we let it be highlighted
    // by some key press - applicable to Center cube only
    // 
    // to make the game more fair, first move should not select the center cube
    // (we don't need to enforce this in code)
    void Update()
    {

    }
}
