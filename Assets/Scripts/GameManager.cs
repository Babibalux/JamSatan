using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public MortalManager mortalManager;
    public DialogueManager dialogueManager;
    public UIMortalSheet UISheetMana;

    public MortalSheetSO actualMortal;

    [HideInInspector] public int askedFeatureID;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void Start()
    {
        Init();
    }

    void Init()
    {
        DealManager.instance.onSealTheDeal.AddListener(FinishActualMortal);

        actualMortal.Init();
        UISheetMana.RefreshSheet();
    }

    void FinishActualMortal()
    {
        DealManager.instance.CalculateScore();
        NextMortal();
    }

    void NextMortal()
    {

        actualMortal.Init();
    }

    #region QuestionSystem
    public void BringUpTopicMortal(int featureID)
    {
        if(actualMortal.questionsRepertory[actualMortal.mortalFeatures[featureID].questionId] != null)
        {
            askedFeatureID = featureID;

            dialogueManager.ChangeDialogue(actualMortal.questionsRepertory[actualMortal.mortalFeatures[featureID].questionId]);
        }
    }
    public void AskQuestionMortal(int buttonID)
    {
        int dialogID = actualMortal.questionsRepertory[actualMortal.mortalFeatures[askedFeatureID].questionId].questions[buttonID].answerDialogueID;
        dialogueManager.ChangeDialogue(actualMortal.questionsRepertory[dialogID]);
    }
    #endregion
}
