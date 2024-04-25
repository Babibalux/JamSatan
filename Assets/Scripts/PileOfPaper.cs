using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PileOfPaper : MonoBehaviour
{
    public UnityEvent onClic;
    public bool isEnabled=false;

    public void Start()
    {
        GameManager.instance.IntroDialogStart.AddListener(DisableInteraction);
        GameManager.instance.IntroDialogEnded.AddListener(EnableInteraction);
    }

    void EnableInteraction()
    {
        isEnabled = true;
    }

    void DisableInteraction()
    {
        isEnabled = false;
    }

    public void OnMouseDown()
    {
        if (isEnabled)
        {
            onClic.Invoke();
            Debug.Log("Pile of paper");
        }
    }
}
