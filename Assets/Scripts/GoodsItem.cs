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

    public bool consumed = false;

    Vector2 spawnPos;

    public void Awake()
    {
        Init();
        //ResetGoods();
    }

    public void Init()
    {
        spawnPos = gameObject.transform.position;
    }

    [ContextMenu("Reset")]
    public void ResetGoods()
    {
        spriteRenderer.sprite = type.logo;
        gameObject.transform.position = spawnPos;

        if(type.startHidden)
        {
            ShowGoods(false);
            if (!consumed)
            {
                onDestroy.Invoke();
            }
        }
        else
        {
            ShowGoods(true,true);
        }

        consumed = false;
    }

    public void ShowGoods(bool show, bool resetPos = false)
    {
        spriteRenderer.enabled = show;
        GetComponent<Collider2D>().enabled = show;
        rb.simulated = show;
        if(resetPos) gameObject.transform.position = spawnPos;
    }

    public void DestroyGoods()
    {    
        destroy = false;
        destroyTime = 0;

        destroyFX.SetActive(false);

        if (!type.doesRespawnInScene)
        {
            ShowGoods(false);
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

    public void SpawnDespawnFX()
    {

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
