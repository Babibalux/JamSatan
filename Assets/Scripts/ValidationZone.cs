using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidationZone : MonoBehaviour
{
    public GoodsSO.SideType authorizedType;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GoodsItem goods = GetGoodsItem(collision.gameObject);

        if(goods != null && goods.type.goodsType != authorizedType)
        {
            goods.WrongZoneProcessus(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    GoodsItem GetGoodsItem(GameObject item)
    {
        GoodsItem goods = item.GetComponent<GoodsItem>();

        return goods;
    }
}
