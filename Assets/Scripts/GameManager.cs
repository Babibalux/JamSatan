using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public MortalManager mortalManager;

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

    public void Start()
    {
        Init();
    }

    void Init()
    {
        DealManager.instance.onSealTheDeal.AddListener(FinishActualMortal);
    }

    void FinishActualMortal()
    {
        DealManager.instance.CalculateScore();
        //NextMortal
    }

    void NextMortal()
    {

    }
}
