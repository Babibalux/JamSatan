using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string dialogueContent;

    [Header("Feature Unlock")]
    public int featureToUnlock = -1;
    public MortalFeature.FeatureType featureUnlockType;
    [Header("Feature Update")]
    public int featureToUpdateID = -1;

    [Header("Unlock Goods")]
    public bool unlockGoods = false;
    public int goodsToUnlockFeatureID;

    [Header("Mortal Expression")]
    public int facialExpression = 0;
    public float expressionDuration = 1f;

    [Header("Is Question")]
    public bool isQuestion = false;
    public Question[] questions;    

    public void Init()
    {
        for(int i = 0; i < questions.Length; i++)
        {
            questions[i].hasBeenAnswered = false;
        }
    }
}
