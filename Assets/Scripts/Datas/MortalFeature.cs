using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MortalFeature
{
    [System.Serializable]
    public enum FeatureType { Name, History, Talent, Sin}
    [System.NonSerialized] public string displayedText;
    public string initialText;

    [Header("Associated Goods")]
    public GoodsSO goods;

    [Header("Link Questions")]
    public int questionId;

    [Header("UpdateFeature")]
    public bool isUpdatable = false;
    public bool isUpdated = false;
    public string updateText;
    public MortalFeature updatedFeature;

    public void FeatureInit()
    {
        displayedText = initialText;
    }

    public void UpdateFeature()
    {
        if(isUpdatable)
        {
            isUpdated = true;
            displayedText = updateText;
        }
    }
}