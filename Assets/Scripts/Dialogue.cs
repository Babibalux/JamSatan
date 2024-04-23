using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string dialogueContent;

    public bool isQuestion = false;
    public Question[] questions;    
}
