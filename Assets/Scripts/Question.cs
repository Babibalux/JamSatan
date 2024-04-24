using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public int answerDialogueID;

    public bool answerOnceOnly;
    public bool hasBeenAnswered;
}
