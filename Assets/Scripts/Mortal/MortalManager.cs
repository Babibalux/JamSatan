using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalManager : MonoBehaviour
{
    public MortalGraphicManager mortalGraphMana;
    public Animator animator;
    public GameObject[] mortals;

    public void InvokeNewMortal(int newMortalID)
    {
        EnableGoodVisual(newMortalID);
        mortalGraphMana = mortals[newMortalID].GetComponent<MortalGraphicManager>();
        animator.SetBool("MortalIn",true);
    }

    public void GetOutMortal()
    {
        animator.SetBool("MortalIn", false);
    }

    void EnableGoodVisual(int ID)
    {
        for(int i = 0; i < mortals.Length; i++)
        {
            if(i == ID)
            {
                mortals[i].SetActive(true);
            }
            else
            {
                mortals[i].SetActive(false);
            }
        }
    }

    public void StartIntroDialog()
    {
        GameManager.instance.ReadNextIntroDialog();
    }
}
