using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DealManager : MonoBehaviour
{
    public GoodsManager goodsManager;

    public BoxCollider2D mortalValidationZone;
    public BoxCollider2D satanValidationZone;

    public AnimationCurve weightScoreByBalance;
    public float minWeight = -3;
    public float maxWeight = 3;

    public UnityEvent onSealTheDeal;

    public static DealManager instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        Init();
    }

    void Init()
    {

    }

    public void SealTheDeal()
    {
        onSealTheDeal.Invoke();
    }

    [ContextMenu("CalculateScore")]
    public int CalculateScore()
    {
        List<GoodsItem> mortalDonation = goodsManager.GetGoodsInZone(mortalValidationZone);
        List<GoodsItem> satanDonation = goodsManager.GetGoodsInZone(satanValidationZone);

        MortalSheetSO actualMortal = GameManager.instance.actualMortal;

        int score = 0;
        float weightBalance = 0;
        //MortalItemsScore
        foreach (GoodsItem item in mortalDonation)
        {
            MortalSheetSO.MortalPreference preference = new MortalSheetSO.MortalPreference();
            actualMortal.preferenceRepertory.TryGetValue(item.type.GoodsName,out preference);

            score += Mathf.RoundToInt(preference.scoreMultiplier * item.type.baseScoreValue);
            weightBalance += item.rb.mass;
        }

        //SatanItemsScore
        foreach (GoodsItem item in satanDonation)
        {
            MortalSheetSO.MortalPreference preference = new MortalSheetSO.MortalPreference();
            actualMortal.preferenceRepertory.TryGetValue(item.type.GoodsName, out preference);

            score += Mathf.RoundToInt(preference.scoreMultiplier * item.type.baseScoreValue);
            weightBalance -= item.rb.mass;
        }

        //WeightBalanceScore  
        if(mortalDonation.Count >= 1 || satanDonation.Count >= 1)
        {
            if (weightBalance < minWeight)
            {
                weightBalance = minWeight;
            }
            else if (weightBalance > maxWeight)
            {
                weightBalance = maxWeight;
            }

            score += Mathf.RoundToInt(weightScoreByBalance.Evaluate(weightBalance));
        }

        Debug.Log("Score : " + score);
        return score;
    }    
}