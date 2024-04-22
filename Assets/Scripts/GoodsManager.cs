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
        ShowHideAllGoods(false);
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
            goodsItem.ResetGoods();
        }
    }
    
    public void ShowHideAllGoods(bool show)
    {
        ShowHideGoods(show, goods.ToArray());
    }

    public void ShowHideGoods(bool show, GoodsItem goods)
    {
        goods.ShowGoods(show);
    }
    public void ShowHideGoods(bool show, GoodsItem[] goods)
    {
        foreach(GoodsItem item in goods)
        {
            item.ShowGoods(show);
        }
    }

    public List<GoodsItem> GetGoodsInZone(Collider2D zone)
    {
        List<GoodsItem> goodsInZone = new List<GoodsItem>();

        List<Collider2D> objectsDetected = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        zone.OverlapCollider(contactFilter, objectsDetected);

        foreach(Collider2D collider in objectsDetected)
        {
            if(collider.GetComponent<GoodsItem>() == true)
            {
                goodsInZone.Add(collider.GetComponent<GoodsItem>());
            }
        }

        return goodsInZone;
    }
}
