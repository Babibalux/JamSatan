using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsItem : MonoBehaviour
{
    public GoodsSO type;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    public void Awake()
    {
        ResetGoods();
    }

    public void ResetGoods()
    {
        spriteRenderer.sprite = type.logo;
        rb.mass = type.baseGoodsWeight;
    }
}
