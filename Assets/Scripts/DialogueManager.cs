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

    [Header("Logic")]
    public Dialogue actualDialogue;

    public void Start()
    {
        SetDialogueActive(false);
    }

    #region Dialogue
    public void ChangeDialogue(Dialogue newDialogue)
    {
        SetDialogueActive(true);

        actualDialogue = newDialogue;
        ChangeText(dialogueText, actualDialogue.dialogueContent);

        if(actualDialogue.isQuestion)
        {
            for(int i = 0; i < askButtons.Length; i++)
            {
                if(i < actualDialogue.questions.Length && !actualDialogue.questions[i].hasBeenAnswered)
                {
                    askButtonsTexts[i].text = actualDialogue.questions[i].questionText;
                    askButtons[i].SetActive(true);
                }
                else
                {
                    askButtonsTexts[i].text = "";
                    askButtons[i].SetActive(false);
                }
            }
        }
    }
    public void SetDialogueActive(bool set)
    {
        dialogueParent.SetActive(set);
    }
    public void ChangeText(TextMeshProUGUI target,string newText)
    {
        target.text = newText;
    }
    #endregion

    #region Ask
    public void Ask(int value)
    {

    }
    #endregion
}
