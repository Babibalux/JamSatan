using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Mortal/MortalFeature", order = 2)]
public class MortalFeatureSO : ScriptableObject
{
    public string displayedText;

    [Header("Associated Goods")]
    public GoodsSO goods;

    [Header("UpdateFeature")]
    public bool isUpdatable = false;
    public string updateText;
    public MortalFeatureSO updatedFeature;
}