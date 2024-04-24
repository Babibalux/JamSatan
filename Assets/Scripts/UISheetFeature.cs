using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UISheetFeature : MonoBehaviour, IPointerDownHandler
{
    public int featureID;
    public TextMeshProUGUI textMesh;

    private bool isDisable = false;

    public void InitFeatureUI(int ID)
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        featureID = ID;

        textMesh.text = GameManager.instance.actualMortal.mortalFeatures[featureID].displayedText;

        if(GameManager.instance.actualMortal.mortalFeatures[featureID].startsHiden)
        {
            SetEnable(false);
        }
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        textMesh.text = GameManager.instance.actualMortal.mortalFeatures[featureID].displayedText;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDisable && GameManager.instance.actualMortal.mortalFeatures[featureID].questionId != -1)
        {
            GameManager.instance.BringUpTopicMortal(featureID);
        }
        //TEST Actualisation Feature
        //GameManager.instance.actualMortal.mortalFeatures[featureID].UpdateFeature();
        //GameManager.instance.UISheetMana.RefreshSheet();
    }

    [ContextMenu("SetEnable")]
    public void TestShow()
    {
        SetEnable(true);
    }
    public void SetEnable(bool set)
    {
        isDisable = !set;
        textMesh.enabled = set;
        Refresh();
    }
}
