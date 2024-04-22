using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewGoods", menuName = "ScriptableObjects/Goods", order = 1)]
public class GoodsSO : ScriptableObject
{
    public string GoodsName;

    public enum SideType { Satan, Mortal}
    public SideType goodsType;

    public float baseGoodsWeight = 1;
    public float baseScoreValue = 100;

    public Sprite logo;
}
