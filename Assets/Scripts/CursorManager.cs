using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public GameObject cursor;

    public Sprite pointTexture;
    public Sprite canTakeTexture;
    public Sprite grabbingTexture;

    private Vector2 cursorHotspot;
    public LayerMask contact;
    public Vector2 castExtents = Vector2.one;

    [System.NonSerialized] public bool isGrabbing = false;
    public enum CursorState { pointing, canTake, grabbing }
    public CursorState actualState;

    public void Update()
    {
        if(isGrabbing && Input.GetMouseButtonUp(0) == true)
        {
            isGrabbing = false;
        }
        else if (!isGrabbing && Input.GetMouseButtonDown(0) == true)
        {
            isGrabbing = true;
        }

        MoveCursor();

        CheckForPickable();
        SwitchCursorState();
    }

    void MoveCursor()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = new Vector2(mousePos.x,mousePos.y);
    }

    void CheckForPickable()
    {
        RaycastHit2D hit = Physics2D.BoxCast(cursor.transform.position, castExtents,0,Vector2.up, contact);

        if(isGrabbing)
        {
            actualState = CursorState.grabbing;
        }
        else if(hit == true && hit.transform.CompareTag("Grabbable"))
        {
            actualState = CursorState.canTake;
        }
        else
        {
            actualState = CursorState.pointing;
        }
    }

    public void SwitchCursorState()
    {
        Sprite newTexture = pointTexture;

        switch (actualState)
        {
            case CursorState.pointing:
                newTexture = pointTexture;
                break;
            case CursorState.canTake:
                newTexture = canTakeTexture;
                break;
            case CursorState.grabbing:
                newTexture = grabbingTexture;
                break;
        }

        cursor.GetComponent<Image>().sprite = newTexture;
    }   
}
