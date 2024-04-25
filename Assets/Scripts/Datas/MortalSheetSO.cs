using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "MortalSheet", menuName = "ScriptableObjects/Mortal/MortalSheet", order = 3)]
public class MortalSheetSO : ScriptableObject
{
    public string mortalID;
    public List<MortalFeature> mortalFeatures;

    public List<MortalPreference> mortalPreferences;
    public Dictionary<string, MortalPreference> preferenceRepertory = new Dictionary<string, MortalPreference>();

    public List<Dialogue> introDialogs;
    public List<Dialogue> dialogsRepertory;

    [System.Serializable]
    public struct MortalPreference
    {
        public GoodsSO goods;
        public float score;
    }


    public int[] satisfactionScores = new int[4];

    public void Init()
    {
        SetPreferencesDictionary();
        FeaturesInit();
        DialogsInit();
    }

    void SetPreferencesDictionary()
    {
        Debug.Log(mortalID + " : SetPreferencesDictionary");
        preferenceRepertory.Clear();
        foreach (MortalPreference mp in mortalPreferences)
        {
            preferenceRepertory.Add(mp.goods.GoodsName, mp);
        }
    }
    void FeaturesInit()
    {
        foreach (MortalFeature feature in mortalFeatures)
        {
            feature.FeatureInit();
        }
    }

    void DialogsInit()
    {
        foreach (Dialogue dialog in dialogsRepertory)
        {
            dialog.Init();
        }
    }
}
