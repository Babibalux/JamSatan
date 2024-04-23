using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class QuestionAsked : UnityEvent<int>
{
}

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueParent;
    public TextMeshProUGUI dialogueText;

    public GameObject buttonParent;
    public GameObject[] askButtons;
    public TextMeshProUGUI[] askButtonsTexts;


    public QuestionAsked questionAskedEvent;

    #region Dialogue
    public void ShowDialogue(bool show)
    {

    }
    public void ChangeText(TextMeshProUGUI target,string newText)
    {
        target.text = newText;
    }
    #endregion

    #region Ask
    public void Ask(int value)
    {
        questionAskedEvent.Invoke(value);
    }
    #endregion
}
