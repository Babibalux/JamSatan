using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public Collider2D mortalZone;
    public Collider2D satanZone;

    public GameObject satanEyes;
    public ParticleSystem eyesOnFX;

    void FixedUpdate()
    {
        if (CheckWeights() == 0)
        {
            AuthorizeDeal(true);
        }
        else AuthorizeDeal(false);
    }

    int CheckWeights()
    {
        int balance = 0;

        List<GoodsItem> mortalItems = GoodsManager.instance.GetGoodsInZone(mortalZone);
        List<GoodsItem> satanItems = GoodsManager.instance.GetGoodsInZone(satanZone);

        List<GoodsItem> tempItems = new List<GoodsItem>();
        for (int i = 0; i < mortalItems.Count; i++)
        {
            if(mortalItems[i].type.goodsType == GoodsSO.SideType.Mortal)
            {
                tempItems.Add(mortalItems[i]);
            }
        }
        mortalItems = tempItems;

        tempItems = new List<GoodsItem>();
        for (int i = 0; i < satanItems.Count; i++)
        {
            if (satanItems[i].type.goodsType == GoodsSO.SideType.Satan)
            {
                tempItems.Add(satanItems[i]);
            }
        }
        satanItems = tempItems;

        if (satanItems.Count > 0 && mortalItems.Count > 0)
        {
            balance = mortalItems.Count - satanItems.Count;
        }
        else balance = -1;

        return balance;
    }
    void AuthorizeDeal(bool set)
    {
        DealManager.instance.dealAuthorized = set;

        satanEyes.gameObject.SetActive(set);
        if (set)
        {
            if(!eyesOnFX.isPlaying) eyesOnFX.Play();
        }
        else if (eyesOnFX.isPlaying) eyesOnFX.Stop();
    }
}
