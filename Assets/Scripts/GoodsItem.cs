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

    [ContextMenu("Reset")]
    public void ResetGoods()
    {
        spriteRenderer.sprite = type.logo;
        rb.mass = type.baseGoodsWeight;
        gameObject.transform.localScale = Vector2.one;
    }

    public void ShowGoods(bool show)
    {
        gameObject.SetActive(show);
    }

    [ContextMenu("TESTWEIGHT")]
    public void TestChangeWeight1()
    {
        ChangeWeight(0.5f,0.75f);
    }
    [ContextMenu("TESTWEIGHT2")]
    public void TestChangeWeight2()
    {
        ChangeWeight(1.5f, 1.25f);
    }

    public void ChangeWeight(float weightModifier, float scaleModifier)
    {
        rb.mass *= weightModifier;
        gameObject.transform.localScale *= scaleModifier;
    }

    #region WrongZone
    public void WrongZoneProcessus(bool set)
    {
        if(set)
        {
            destroy = true;
            StartCoroutine(SelfDestroyProcessus());
            destroyFX.SetActive(true);
        }
        else
        {
            destroyTime = 0;
            destroy = false;

            destroyFX.SetActive(false);
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
                StartCoroutine(SelfDestroyProcessus());
            }
            else if (destroyTime >= destroyDuration)
            {
                destroy = false;
                destroyTime = 0;
                gameObject.SetActive(false);

                destroyFX.SetActive(false);
            }
        }
    }
    #endregion
}
