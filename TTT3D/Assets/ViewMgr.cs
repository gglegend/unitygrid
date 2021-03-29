using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMgr : MonoBehaviour
{
    Vector3 origin;

    void Start()
    {
        origin = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        // WASD keys rotate the object (around the origin)
        // ..

        // optionally, arrow keys move the object left-right, up-down
        //..

        // optionally, re-center upon some key press:
        // same idea as in Hockey puck

    }
}
