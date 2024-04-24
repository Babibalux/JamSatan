using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMaskFollow : MonoBehaviour
{
    public GameObject cursor;

    void Update()
    {
        MoveCursor();
    }

    void MoveCursor()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = new Vector2(mousePos.x, mousePos.y);
    }
}
