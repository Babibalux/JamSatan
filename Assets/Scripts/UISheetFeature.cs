using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UISheetFeature : MonoBehaviour, IPointerDownHandler
{
    public int featureID;
    public TextMeshProUGUI textMesh;

    public void InitFeatureUI(int ID)
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        featureID = ID;

        textMesh.text = GameManager.instance.actualMortal.mortalFeatures[featureID].displayedText;
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        textMesh.text = GameManager.instance.actualMortal.mortalFeatures[featureID].displayedText;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.AskMortal(featureID);

        //TEST Actualisation Feature
        //GameManager.instance.actualMortal.mortalFeatures[featureID].UpdateFeature();
        //GameManager.instance.UISheetMana.RefreshSheet();
    }
}
