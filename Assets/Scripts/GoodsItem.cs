using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsItem : MonoBehaviour
{
    public GoodsSO type;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    [Header("Destruction")]
    public bool destroy;
    public float destroyDuration = 1f;
    public GameObject destroyFX;
    float destroyTime;

    public void Awake()
    {
        ResetGoods();
    }

    public void ResetGoods()
    {
        spriteRenderer.sprite = type.logo;
        rb.mass = type.baseGoodsWeight;
    }

    public void ShowGoods(bool show)
    {
        gameObject.SetActive(show);
    }

    public void WrongZoneProcessus(bool set)
    {
        if(set)
        {
            destroy = true;
            StartCoroutine(SelfDestroyProcessus());
        }
        else
        {
            destroy = false;
        }
    }

    IEnumerator SelfDestroyProcessus()
    {
        yield return new WaitForFixedUpdate();
                
        if(destroy)
        {
            if (destroyTime < destroyDuration)
            {
                destroyTime += Time.fixedDeltaTime;
            }
            else if (destroyTime >= destroyDuration)
            {
                destroy = false;
                gameObject.SetActive(false);
            }
            StartCoroutine(SelfDestroyProcessus());
        }
    }
}
