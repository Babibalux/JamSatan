using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    Vector2 difference = Vector2.zero;
    Rigidbody2D rb;
    bool hasRb = false;

    public void Awake()
    {
        if(GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
            hasRb = true;
        }
    }

    private void OnMouseDown()
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
        SetRigidBody(false);
    }

    public void OnMouseUp()
    {
        SetRigidBody(true);
    }

    void SetRigidBody(bool set)
    {
        if(hasRb)
        {
            if(set == false)
            {
                rb.Sleep();
            }
            else
            {
                rb.WakeUp();
            }
        }
    }
}
