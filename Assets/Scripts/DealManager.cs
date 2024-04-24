using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DealManager : MonoBehaviour
{
    public GoodsManager goodsManager;

    public BoxCollider2D mortalValidationZone;
    public BoxCollider2D satanValidationZone;

    public bool dealAuthorized = false;

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
    }

    public void SealTheDeal()
    {
        List<GoodsItem> consumedGoods = goodsManager.GetGoodsInZone(mortalValidationZone);
        List<GoodsItem> satanGoods = goodsManager.GetGoodsInZone(satanValidationZone);

        onSealTheDeal.Invoke();

        foreach (GoodsItem goods in consumedGoods)
        {
            goods.consumed = true;
            goods.ShowGoods(false);
        }
        foreach (GoodsItem goods in satanGoods)
        {
            goods.consumed = true;
            goods.ShowGoods(false);
        }
    }

    [ContextMenu("CalculateScore")]
    public int CalculateScore()
    {
        List<GoodsItem> mortalDonation = goodsManager.GetGoodsInZone(mortalValidationZone);
        List<GoodsItem> satanDonation = goodsManager.GetGoodsInZone(satanValidationZone);

        MortalSheetSO actualMortal = GameManager.instance.actualMortal;

        int score = 0;
        //MortalItemsScore
        for (int i = 0; i < mortalDonation.Count; i++)
        {
            MortalSheetSO.MortalPreference preference = new MortalSheetSO.MortalPreference();
            actualMortal.preferenceRepertory.TryGetValue(mortalDonation[i].type.GoodsName,out preference);

            score += Mathf.RoundToInt(preference.score);
        }

        //SatanItemsScore
        for (int i = 0; i < satanDonation.Count; i++)
        {
            MortalSheetSO.MortalPreference preference = new MortalSheetSO.MortalPreference();
            actualMortal.preferenceRepertory.TryGetValue(satanDonation[i].type.GoodsName, out preference);

            score += Mathf.RoundToInt(preference.score);            
        }

        Debug.Log("Score : "+ score);
        return score;
    }    
}