using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "MortalSheet", menuName = "ScriptableObjects/Mortal/MortalSheet", order = 3)]
public class MortalSheetSO : ScriptableObject
{
    public string mortalID;
    public List<MortalFeature> mortalFeatures;

    public List<MortalPreference> mortalPreferences;
    public Dictionary<string,MortalPreference> preferenceRepertory = new Dictionary<string, MortalPreference>();

    public List<Dialogue> questionsRepertory;

    [System.Serializable]
    public struct MortalPreference
    {
        public GoodsSO goods;
        public float scoreMultiplier;
        public float goodsModifiedWeightMultiplier;
        public float goodsModifiedSizeMultiplier;
    }

    public void Init()
    {
        SetPreferencesDictionary();
        FeaturesInit();
    }

    void SetPreferencesDictionary()
    {
        foreach(MortalPreference mp in mortalPreferences)
        {
            preferenceRepertory.Add(mp.goods.GoodsName,mp);
        }
    }
    void FeaturesInit()
    {
        foreach(MortalFeature feature in mortalFeatures)
        {
            feature.FeatureInit();
        }
    }
}
