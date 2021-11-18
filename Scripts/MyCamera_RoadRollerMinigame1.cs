using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera_RoadRollerMinigame1 : MonoBehaviour
{
    private BoxCollider2D col;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        //col.size = new Vector2(2 * ((Screen.width * 1.0f) / Screen.height) * GetComponent<Camera>().orthographicSize, 2 * GetComponent<Camera>().orthographicSize);
        col.offset = new Vector2(-1 * ((Screen.width * 1.0f) / Screen.height) * GetComponent<Camera>().orthographicSize - 3.5f, col.offset.y);
    }
}
