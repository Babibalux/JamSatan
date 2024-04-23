using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalManager : MonoBehaviour
{
    public MortalGraphicManager mortalGraphMana;

    public void InvokeNewMortal(GameObject newMortal)
    {
        Instantiate(newMortal, this.transform);
        mortalGraphMana = newMortal.GetComponent<MortalGraphicManager>();
    }

    public void GetOutMortal()
    {
        Destroy(mortalGraphMana.gameObject);
    }
}
