using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UISheetFeature : MonoBehaviour
{
    public int featureID;
    public TextMeshProUGUI textMesh;
    public GameObject button;

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

    public void Ask()
    {
        if (!isDisable && GameManager.instance.actualMortal.mortalFeatures[featureID].questionId != -1)
        {
            GameManager.instance.BringUpTopicMortal(featureID);
        }
    }

    [ContextMenu("SetEnable")]
    public void TestShow()
    {
        SetEnable(true);
    }
    public void SetEnable(bool set)
    {
        isDisable = !set;
        button.SetActive(set);
        Refresh();
    }
}
