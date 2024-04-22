using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealManager : MonoBehaviour
{
    public GoodsManager goodsManager;

    public BoxCollider2D mortalValidationZone;
    public BoxCollider2D satanValidationZone;

    public static DealManager instance { get; private set; }
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
    }

    [ContextMenu("CalculateScore")]
    public void CalculateScore()
    {

    }    

    public void CheckGoodsValidity()
    {

    }
}