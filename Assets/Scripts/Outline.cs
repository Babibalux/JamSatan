using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    bool outlineOn = false;
    SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseOver()
    {
        if(!outlineOn)
        {
            spriteRenderer.material.SetColor("_OutlineColor", Color.white);
        }

        outlineOn = true;
    }

    public void OnMouseExit()
    {
        spriteRenderer.material.SetColor("_OutlineColor", Color.black);
        outlineOn = false;
    }
}
