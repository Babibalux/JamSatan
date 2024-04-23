using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PileOfPaper : MonoBehaviour
{
    public UnityEvent onClic;

    public void OnMouseDown()
    {
        onClic.Invoke();
    }
}
