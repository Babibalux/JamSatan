using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager : MonoBehaviour
{
    public GameObject regularGoodsParent;

    public List<GoodsItem> regularGoods;
    public Dictionary<string, GoodsItem> goodsRepertory = new Dictionary<string, GoodsItem>();


    public GameObject specialGoodsParent;

    public List<GoodsItem> mortalSpecialGoods;
    public Dictionary<string, GoodsItem> specialGoodsRepertory = new Dictionary<string, GoodsItem>();

    public static GoodsManager instance { get; private set; }
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
        regularGoods = GetAllGoods(regularGoodsParent);
        SetGoodsRepertory();
        ShowHideAllRegularGoods(false);
        StartHiddenObjects(regularGoods);
    }

    List<GoodsItem> GetAllGoods(GameObject parent)
    {
        List<GoodsItem> target = new List<GoodsItem>();
        GoodsItem[] items = parent.GetComponentsInChildren<GoodsItem>();

        foreach(GoodsItem item in items)
        {
            target.Add(item);
        }

        return target;
    }

    void StartHiddenObjects(List<GoodsItem> goodsItems)
    {
        foreach(GoodsItem goods in goodsItems)
        {
            if(!goods.type.startHidden)
            {
                ShowHideGoods(true, goods);
            }
        }
    }

    void SetGoodsRepertory()
    {
        foreach (GoodsItem goodsItem in regularGoods)
        {
            goodsRepertory.Add(goodsItem.type.name, goodsItem);
        }
    }
    void SetSpecialGoodsRepertory()
    {
        foreach (GoodsItem goodsItem in mortalSpecialGoods)
        {
            specialGoodsRepertory.Add(goodsItem.type.name, goodsItem);
        }
    }

    public void ActualizeGoodsWeight(string targetName, float newWeight, bool isRegularGoods = true)
    {
        GoodsItem target;
        if(isRegularGoods)
        {
            goodsRepertory.TryGetValue(targetName, out target);
        }
        else
        {
            specialGoodsRepertory.TryGetValue(targetName, out target);
        }

        target.rb.mass = newWeight;
    }

    public void ResetAllGoods()
    {
        foreach(GoodsItem goodsItem in regularGoods)
        {
            goodsItem.ResetGoods();
        }
    }
    
    public void ShowHideAllRegularGoods(bool show)
    {
        ShowHideGoods(show, regularGoods.ToArray());
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
