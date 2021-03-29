using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public Text label;

    protected static Color RED = new Color(1f, 0f, 0f, 0.5f);
    protected static Color BLUE = new Color(0f, 0f, 1f, 0.5f);
    protected Renderer rend;

    // Modify transparenct or opacity (alpha) of a cube:
    // choose a pair of keys to modify these - 
    // should be applicable to an empty cube only
    // (only for empty cubes)

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // On mouse click, paint empty cube RED or BLUE, depending on 
    // whose turn is it... also update the "turn" label so the players
    // could see whose turn it is
}
