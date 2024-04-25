using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public MortalManager mortalManager;
    public DialogueManager dialogueManager;
    public UIMortalSheet UISheetMana;
    public CursorManager cursorManager;

    public MortalSheetSO actualMortal;

    public MortalSheetSO[] allMortals;

    [HideInInspector] public int askedFeatureID;

    public UnityEvent IntroDialogStart;
    public UnityEvent IntroDialogEnded;

    public int mortalIndex = -1;
    int introDialogIndex = 0;

    [Header("Score System")]
    public int actualScore = 0;

    public Animator devilHand;
    bool firstGame = true;


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
        if(firstGame) DealManager.instance.onSealTheDeal.AddListener(FinishActualMortal);
        firstGame = false;
        actualScore = 0;

        GoodsManager.instance.Init();

        UISheetMana.RefreshSheet();
    }

    void FinishActualMortal()
    {
        int scoreGained = DealManager.instance.CalculateScore();

        AddScore(scoreGained);
        introDialogIndex = 0;

        ScoreMortalReaction(scoreGained);

        UISheetMana.ShowSheet(false);
        dialogueManager.SetDialogueActive(false);

        //ResetGoods
        devilHand.SetTrigger("Next");

        mortalManager.GetOutMortal();
        Invoke("NextMortal",4f);
    }

    //2 3 0 1
    void ScoreMortalReaction(int score)
    {
        if(score <= actualMortal.satisfactionScores[0])
        {
            mortalManager.mortalGraphMana.ChangeExpression(2,0,4);
        }
        else if (score <= actualMortal.satisfactionScores[1])
        {
            mortalManager.mortalGraphMana.ChangeExpression(3, 0, 4);
        }
        else if (score <= actualMortal.satisfactionScores[2])
        {
            mortalManager.mortalGraphMana.ChangeExpression(0);
        }
        else if (score >= actualMortal.satisfactionScores[3])
        {
            mortalManager.mortalGraphMana.ChangeExpression(1,0,4);
        }
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

            mortalIndex = -1;
            MacroManager.instance.ScoreMenu(actualScore);
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
        
        if (introDialogIndex == 0)
        {
            IntroDialogStart.Invoke();
        }

        if (introDialogIndex < actualMortal.introDialogs.Count -1)
        {
            dialogueManager.NextIntroDialog(actualMortal.introDialogs[introDialogIndex], false);
        }
        else
        {
            dialogueManager.NextIntroDialog(actualMortal.introDialogs[introDialogIndex], true);
        }
        introDialogIndex++;

        if (introDialogIndex == actualMortal.introDialogs.Count)
        {
            introDialogIndex = 0;

            IntroDialogEnded.Invoke();
        }
    }

    public void AddScore(int value)
    {
        actualScore += value;
    }
}
