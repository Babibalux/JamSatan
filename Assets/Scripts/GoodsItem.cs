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

    public void ShowGoods(bool show, bool resetPos = false, bool showRespawnFX = true)
    {
        spriteRenderer.enabled = show;
        GetComponent<Collider2D>().enabled = show;
        rb.simulated = show;


        if (!show)
        {
            DespawnFX();
        }

        if (resetPos) gameObject.transform.position = spawnPos;

        if (show && showRespawnFX)
        {
            RespawnFX();
        }
    }

    public void DestroyGoods()
    {
        DespawnFX();

        destroy = false;
        destroyTime = 0;

        if (!type.doesRespawnInScene)
        {
            ShowGoods(false);
            onDestroy.Invoke();
        }
        else
        {
            transform.position = Vector2.zero;
            GetComponent<Draggable>().enable = false;
            RespawnFX();
            StartCoroutine(ManipulationCooldown());
        }
    }

    IEnumerator ManipulationCooldown()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<Draggable>().enable = true;
    }

    public void RespawnFX()
    {
        GameObject FXObject = Instantiate<GameObject>(respawnFX, this.gameObject.transform.position, Quaternion.identity);

        StartCoroutine(SpawnFXDisable(FXObject));
    }

    IEnumerator SpawnFXDisable(GameObject FXObject)
    {
        yield return new WaitForSeconds(5f);
        Destroy(FXObject);
    }

    public void DespawnFX()
    {
        GameObject FXObject = Instantiate<GameObject>(destroyFX, this.gameObject.transform.position, Quaternion.identity);

        StartCoroutine(DespawnFXDisable(FXObject));
    }

    IEnumerator DespawnFXDisable(GameObject FXObject)
    {
        yield return new WaitForSeconds(5f);
        Destroy(FXObject);
    }

    #region WrongZone
    public void WrongZoneProcessus(bool set)
    {
        if(set)
        {
            destroy = true;
            StartCoroutine(SelfDestroyProcessus());
            //destroyFX.SetActive(true);
        }
        else
        {
            destroyTime = 0;
            destroy = false;
            //destroyFX.SetActive(false);
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
