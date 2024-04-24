using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public GameObject managersParents;
    public MortalManager mortalManager;
    public DialogueManager dialogueManager;
    public UIMortalSheet UISheetMana;
    public CursorManager cursorManager;

    public MortalSheetSO actualMortal;

    public MortalSheetSO[] allMortals;

    [HideInInspector] public int askedFeatureID;

    public UnityEvent IntroDialogStart;
    public UnityEvent IntroDialogEnded;

    int mortalIndex = -1;
    int introDialogIndex = 0;

    [Header("Score System")]
    public int actualScore = 0;

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



    public void StartGame()
    {
        Init();
        NextMortal();
    }

    void Init()
    {
        DealManager.instance.onSealTheDeal.AddListener(FinishActualMortal);
        actualScore = 0;

        GoodsManager.instance.Init();

        UISheetMana.RefreshSheet();
    }

    void FinishActualMortal()
    {
        AddScore(DealManager.instance.CalculateScore());
        UISheetMana.ShowSheet(false);

        //ResetGoods

        mortalManager.GetOutMortal();
        Invoke("NextMortal",3f);
    }

    void NextMortal()
    {
        mortalIndex++;

        //There are still mortals to deal with
        if (mortalIndex < allMortals.Length)
        {
            actualMortal = allMortals[mortalIndex];


            actualMortal.Init();
            UISheetMana.ResetFeatures();
            UISheetMana.FeaturesUISetUp(actualMortal.mortalFeatures);
            UISheetMana.RefreshSheet();

            GoodsManager.instance.ResetAllGoods();

            mortalManager.InvokeNewMortal(mortalIndex);
        }
        else //It was the last mortal
        {
            dialogueManager.SetDialogueActive(false);
            UISheetMana.ShowSheet(false);

            MacroManager.instance.ScoreMenu(actualScore);
            mortalIndex = -1;
        }
    }

    #region QuestionSystem
    public void BringUpTopicMortal(int featureID)
    {
        if(actualMortal.dialogsRepertory[actualMortal.mortalFeatures[featureID].questionId] != null)
        {
            askedFeatureID = featureID;

            dialogueManager.ChangeDialogue(actualMortal.dialogsRepertory[actualMortal.mortalFeatures[featureID].questionId]);
        }
    }
    public void AskQuestionMortal(int buttonID)
    {
        int dialogID = actualMortal.dialogsRepertory[actualMortal.mortalFeatures[askedFeatureID].questionId].questions[buttonID].answerDialogueID;
        dialogueManager.ChangeDialogue(actualMortal.dialogsRepertory[dialogID]);

        if(actualMortal.dialogsRepertory[actualMortal.mortalFeatures[askedFeatureID].questionId].questions[buttonID].answerOnceOnly) 
            actualMortal.dialogsRepertory[actualMortal.mortalFeatures[askedFeatureID].questionId].questions[buttonID].hasBeenAnswered = true;
    }
    #endregion

    #region GoodsManagement
    public void AddGoods(string goodsName)
    {
        GoodsManager.instance.ShowHideGoods(true,goodsName, true);
    }
    #endregion

    public void ReadNextIntroDialog()
    {
        if(introDialogIndex == 0)
        {
            IntroDialogStart.Invoke();
        }

        if (introDialogIndex < actualMortal.introDialogs.Count -1)
        {
            dialogueManager.NextIntroDialog(actualMortal.introDialogs[introDialogIndex], false);
            introDialogIndex++;
        }
        else
        {
            dialogueManager.NextIntroDialog(actualMortal.introDialogs[introDialogIndex], true);
            introDialogIndex = 0;

            IntroDialogEnded.Invoke();
        }
    }

    public void AddScore(int value)
    {
        actualScore += value;
    }
}
