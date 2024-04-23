using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Collider2D collider;

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
        renderer.enabled = available;
        collider.enabled = available;
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
