using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISheetFeature : MonoBehaviour
{
    public int featureID;
    public TextMeshProUGUI textMesh;

    public void InitFeatureUI(int ID)
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        featureID = ID;

        textMesh.text = GameManager.instance.actualMortal.mortalFeatures[featureID].displayedText;
    }

    public void Refresh()
    {
        textMesh.text = GameManager.instance.actualMortal.mortalFeatures[featureID].displayedText;
    }

    public void OnMouseDown()
    {
        //CALL LINKED QUESTION ON GAMEMANAGER
    }
}
