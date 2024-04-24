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
    public GameObject closeButton;
    public GameObject nextButton;

    [Header("Logic")]
    public Dialogue actualDialogue;

    public void Start()
    {
        SetDialogueActive(false);
    }

    #region Dialogue
    public void ChangeDialogue(Dialogue newDialogue, bool isStartDialog = false)
    {
        SetDialogueActive(true);

        actualDialogue = newDialogue;
        ChangeText(dialogueText, actualDialogue.dialogueContent);

        if(isStartDialog == false)
        {
            nextButton.SetActive(false);
            closeButton.SetActive(true);

            if (actualDialogue.isQuestion)
            {
                for (int i = 0; i < askButtons.Length; i++)
                {
                    if (i < actualDialogue.questions.Length && !actualDialogue.questions[i].hasBeenAnswered)
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
            else
            {
                for (int i = 0; i < askButtons.Length; i++)
                {
                    askButtonsTexts[i].text = "";
                    askButtons[i].SetActive(false);
                }
            }

            if (actualDialogue.featureToUnlock != -1)
            {
                GameManager.instance.UISheetMana.ShowFeature(actualDialogue.featureUnlockType, actualDialogue.featureToUnlock);
            }

            if (actualDialogue.featureToUpdateID != -1)
            {
                GameManager.instance.actualMortal.mortalFeatures[actualDialogue.featureToUpdateID].UpdateFeature();
                GameManager.instance.UISheetMana.RefreshSheet();
            }

            if (actualDialogue.unlockGoods)
            {
                GameManager.instance.AddGoods(actualDialogue.goodsToUnlock);
            }
        }
        else
        {
            nextButton.SetActive(true);
            closeButton.SetActive(false);

            for (int i = 0; i < askButtons.Length; i++)
            {
                askButtonsTexts[i].text = "";
                askButtons[i].SetActive(false);
            }
        }

        StartCoroutine(ExpressionChange());
    }
    public void SetDialogueActive(bool set)
    {
        dialogueParent.SetActive(set);
    }
    public void ChangeText(TextMeshProUGUI target,string newText)
    {
        target.text = newText;
    }

    IEnumerator ExpressionChange()
    {
        GameManager.instance.mortalManager.mortalGraphMana.ChangeExpression(actualDialogue.facialExpression);
        yield return new WaitForSeconds(actualDialogue.expressionDuration);
        GameManager.instance.mortalManager.mortalGraphMana.ChangeExpression(0);
    }
    #endregion

    public void NextIntroDialog(Dialogue newDialog, bool isLast)
    {
        if(!isLast) ChangeDialogue(newDialog,true);
        else ChangeDialogue(newDialog, false);
    }


    #region Ask
    public void Ask(int value)
    {
        GameManager.instance.AskQuestionMortal(value);
    }
    #endregion
}
