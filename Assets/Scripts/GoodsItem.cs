using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoodsItem : MonoBehaviour
{
    public GoodsSO type;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    [Header("Destruction")]
    public bool destroy;
    public float destroyDuration = 1f;
    public GameObject destroyFX;
    public GameObject respawnFX;
    float destroyTime;
    public UnityEvent onDestroy;
    

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

    public void DestroyGoods()
    {    
        destroy = false;
        destroyTime = 0;

        destroyFX.SetActive(false);

        if (!type.doesRespawnInScene)
        {
            gameObject.SetActive(false);
            onDestroy.Invoke();
        }
        else
        {
            transform.position = Vector2.zero;
            GetComponent<Draggable>().enable = false;
            respawnFX.SetActive(true);
            StartCoroutine(RespawnFXDisable());
        }
    }

    IEnumerator RespawnFXDisable()
    {
        yield return new WaitForSeconds(3f);
        respawnFX.SetActive(false);
        GetComponent<Draggable>().enable = true;
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
                DestroyGoods();
            }
        }
    }
    #endregion
}
