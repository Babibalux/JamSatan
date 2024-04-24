using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMortalSheet : MonoBehaviour
{
    public UISheetFeature nameFeature;

    public GameObject uiSheetParent;

    public GameObject historyList;
    public GameObject historyPrefab;
    public List<UISheetFeature> historyFeatures;

    public GameObject miscList;
    public GameObject miscPrefab;
    public List<UISheetFeature> miscFeatures;

    public void Start()
    {
        ResetFeatures();
        FeaturesUISetUp(GameManager.instance.actualMortal.mortalFeatures);
    }

    #region FeatureManagement
    public void FeaturesUISetUp(List<MortalFeature> features)
    {
        for(int i = 0; i < features.Count; i++)
        {
            AddFeature(features[i], i);
        }
    }

    public void AddFeature(MortalFeature feature, int ID)
    {
        switch(feature.featureType)
        {
            case MortalFeature.FeatureType.Name:
                {                   
                    nameFeature.InitFeatureUI(ID);
                    break;
                }
            case MortalFeature.FeatureType.History:
                {
                    UISheetFeature uiFeature = Instantiate<GameObject>(historyPrefab,historyList.transform).GetComponent<UISheetFeature>();
                    uiFeature.InitFeatureUI(ID);
                    historyFeatures.Add(uiFeature);
                    break;
                }
            case MortalFeature.FeatureType.Misc:
                {
                    UISheetFeature uiFeature = Instantiate<GameObject>(historyPrefab, miscList.transform).GetComponent<UISheetFeature>();
                    uiFeature.InitFeatureUI(ID);
                    miscFeatures.Add(uiFeature);
                    break;
                }
        }
    }

    public void ShowFeature(MortalFeature.FeatureType type, int ID, bool set = true)
    {
        switch(type)
        {
            case MortalFeature.FeatureType.Name:
                nameFeature.SetEnable(set);
                break;
            case MortalFeature.FeatureType.History:
                historyFeatures[ID].SetEnable(set);
                break;
            case MortalFeature.FeatureType.Misc:
                miscFeatures[ID].SetEnable(set);
                break;
        }
    }

    public void RefreshSheet()
    {
        nameFeature.Refresh();

        for(int i = 0; i < historyFeatures.Count; i++)
        {
            historyFeatures[i].Refresh();
        }

        for (int i = 0; i < miscFeatures.Count; i++)
        {
            miscFeatures[i].Refresh();
        }
    }

    public void ResetFeatures()
    {
        ClearFeatureList(historyFeatures);
        ClearFeatureList(miscFeatures);
    }

    public void ClearFeatureList(List<UISheetFeature> list)
    {
        for(int i = 0; i < list.Count;i++)
        {
            Destroy(list[i].gameObject);
        }
        list.Clear();
    }
    #endregion

    public void ShowSheet(bool set)
    {
        uiSheetParent.GetComponent<Animator>().SetBool("isActive",set);
    }
}
