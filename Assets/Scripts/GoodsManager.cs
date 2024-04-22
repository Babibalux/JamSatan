using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager : MonoBehaviour
{
    public List<GoodsItem> goods;
    public Dictionary<string, GoodsItem> goodsRepertory = new Dictionary<string, GoodsItem>();

    public void Awake()
    {
        Init();
    }

    void Init()
    {
        SetGoodsRepertory();
    }

    void SetGoodsRepertory()
    {
        foreach (GoodsItem goodsItem in goods)
        {
            goodsRepertory.Add(goodsItem.type.name, goodsItem);
        }
    }

    public void ActualizeGoodsWeight(string targetName, float newWeight)
    {
        GoodsItem target;
        goodsRepertory.TryGetValue(targetName, out target);

        target.rb.mass = newWeight;
    }

    public void ResetAllGoods()
    {
        foreach(GoodsItem goodsItem in goods)
        {
            goodsItem.rb.mass = goodsItem.type.baseGoodsWeight;
        }
    }
}
