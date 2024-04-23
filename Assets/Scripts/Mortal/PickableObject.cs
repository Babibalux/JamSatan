using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    
    public enum PickableTarget { Null, Eyes, Nose, Mouth, Other}
    public PickableTarget target;

    public Collider2D colliderZone;

    public string linkedGoods;
    private GoodsItem goodsItem;


    public bool available = true;

    public void Start()
    {
        GoodsManager.instance.goodsRepertory.TryGetValue(linkedGoods, out goodsItem);
        goodsItem.onDestroy.AddListener(GoodsDestroyed);
    }

    public void SetActive(bool value)
    {
        available = value;
        
        switch (target)
        {
            case PickableTarget.Eyes:
                GameManager.instance.mortalManager.mortalGraphMana.SetEyes(value);
                break;
            case PickableTarget.Nose:
                GameManager.instance.mortalManager.mortalGraphMana.SetNose(value);
                break;
            case PickableTarget.Mouth:
                GameManager.instance.mortalManager.mortalGraphMana.SetMouth(value);
                break;
            case PickableTarget.Other:
                break;
        }

        colliderZone.enabled = available;
    }

    void GoodsDestroyed()
    {
        SetActive(true);
    }

    public void OnMouseDown()
    {
        SetActive(false);
        goodsItem.ShowGoods(true);
        goodsItem.transform.position = this.transform.position;
    }
}
