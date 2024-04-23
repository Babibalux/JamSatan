using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealBell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    
    public void OnMouseOver()
    {
        if(!animator.GetBool("Hover"))  animator.SetBool("Hover", true);
    }

    public void OnMouseExit()
    {
        animator.SetBool("Hover", false);
    }

    public void OnMouseDown()
    {
        DealManager.instance.SealTheDeal();
        animator.SetTrigger("Cling");
        animator.SetBool("Hover", false);
    }
}
