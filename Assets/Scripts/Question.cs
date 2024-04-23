using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public string answerDialogue;
    public int featureToUpdateID;

    public bool hasBeenAnswered;
}
