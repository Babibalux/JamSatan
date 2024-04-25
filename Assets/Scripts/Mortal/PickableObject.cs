using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    
    public enum PickableTarget { Null, Eyes, Nose, Mouth, Heart, Soul, Accessory, Other}
    public PickableTarget target;

    public Collider2D colliderZone;
    public ContactFilter2D contactFilter;

    public string linkedGoods;
    private GoodsItem goodsItem;

    public bool available = true;

    public void Start()
    {
        GoodsManager.instance.goodsRepertory.TryGetValue(linkedGoods, out goodsItem);
        goodsItem.onDestroy.AddListener(GoodsDestroyed);
    }

    public void FixedUpdate()
    {
        if (!available && Input.GetMouseButtonUp(0))
        {
            List<Collider2D> colliders = new List<Collider2D>();
            colliderZone.OverlapCollider(contactFilter, colliders);

            foreach (Collider2D hit in colliders)
            {
                GoodsItem hitGoods = hit.GetComponentInParent<GoodsItem>();
                if (hitGoods != null)
                {
                    if (goodsItem == hitGoods)
                    {
                        SetActive(true);
                        goodsItem.ShowGoods(false);
                    }
                }
            }
        }
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
            case PickableTarget.Heart:
                GameManager.instance.mortalManager.mortalGraphMana.SetHeart(value);
                break;
            case PickableTarget.Soul:
                GameManager.instance.mortalManager.mortalGraphMana.SetSoul(value);
                break;
            case PickableTarget.Accessory:
                GameManager.instance.mortalManager.mortalGraphMana.SetAccessory(value);
                break;
        }
    }

    void GoodsDestroyed()
    {
        SetActive(true);
    }

    public void OnMouseDown()
    {
        if(available)
        {
            SetActive(false);
            GameManager.instance.mortalManager.mortalGraphMana.ChangeExpression(3, 0, 1.5f);
            goodsItem.ShowGoods(true, false, false);
            goodsItem.transform.position = this.transform.position;
            goodsItem.RespawnFX();
        }
    }
}
