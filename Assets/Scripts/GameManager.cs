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
    public void AskMortal(int featureID)
    {
        dialogueManager.ChangeDialogue(actualMortal.questionsRepertory[actualMortal.mortalFeatures[featureID].questionId]);
    }
    #endregion
}
