using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "MortalSheet", menuName = "ScriptableObjects/Mortal", order = 1)]
public class MortalSheetSO : ScriptableObject
{
    public MortalFeatureSO mortalName;
    public List<MortalFeatureSO> mortalHistory;
    public List<MortalFeatureSO> mortalSkills;
    public List<MortalFeatureSO> mortalSins;

    public List<MortalPreference> mortalPreferences;
    public Dictionary<string,MortalPreference> preferenceRepertory = new Dictionary<string, MortalPreference>();

    [System.Serializable]
    public struct MortalPreference
    {
        public GoodsSO goods;
        public float scoreMultiplier;
        public float goodsModifiedWeight;
    }

    public void Init()
    {
        SetPreferencesDictionary();
    }

    void SetPreferencesDictionary()
    {
        foreach(MortalPreference mp in mortalPreferences)
        {
            preferenceRepertory.Add(mp.goods.GoodsName,mp);
        }
    }
}
