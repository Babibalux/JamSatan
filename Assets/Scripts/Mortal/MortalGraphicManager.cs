using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalGraphicManager : MonoBehaviour
{
    [Header("Eyes")]
    public bool hasEyes = true;
    public SpriteRenderer[] eyes;
    bool eyesOn = true;

    [Header("Nose")]
    public bool hasNose = true;
    public SpriteRenderer nose;
    bool noseOn = true;

    [Header("Mouth")]
    public bool hasMouth = true;
    public SpriteRenderer[] mouth;
    bool mouthOn = true;

    [Header("Heart")]
    public bool hasHeart = true;
    public SpriteRenderer heart;
    bool heartOn = true;

    [Header("Soul")]
    public bool hasSoul = true;
    public SpriteRenderer soul;
    bool soulOn = true;

    [Header("Accessory")]
    public bool hasAccessory = true;
    public SpriteRenderer accessory;
    bool accessoryOn = true;

    public int expression = 0;


    public void FixedUpdate()
    {
        FacialExpression();
    }

    public void ResetMortalGraph()
    {
        SetEyes(true);
        SetNose(true);
        SetMouth(true);
        SetHeart(true);
        SetSoul(true);
        SetAccessory(true);
    }

    void FacialExpression()
    {
        if(hasEyes)
        {
            if (eyesOn)
            {
                eyes[4].enabled = false;
                for (int i = 0; i < eyes.Length - 1; i++)
                {
                    if (i == expression)
                    {
                        eyes[i].enabled = true;
                    }
                    else eyes[i].enabled = false;
                }
            }
            else
            {
                eyes[4].enabled = true;
                for (int i = 0; i < eyes.Length - 1; i++)
                {
                    eyes[i].enabled = false;
                }
            }
        }
        
        if(hasNose)
        {
            if (noseOn) nose.enabled = true;
            else nose.enabled = false;
        }

        if(hasMouth)
        {
            if (mouthOn)
            {
                mouth[4].enabled = false;
                for (int i = 0; i < mouth.Length - 1; i++)
                {
                    if (i == expression)
                    {
                        mouth[i].enabled = true;
                    }
                    else mouth[i].enabled = false;
                }
            }
            else
            {
                mouth[4].enabled = true;
                for (int i = 0; i < mouth.Length - 1; i++)
                {
                    mouth[i].enabled = false;
                }
            }
        }

        if (hasSoul)
        {
            if (soulOn) soul.enabled = true;
            else soul.enabled = false;
        }

        if (hasHeart)
        {
            if (heartOn) heart.enabled = true;
            else heart.enabled = false;
        }

        if (hasAccessory)
        {
            if (accessoryOn) accessory.enabled = true;
            else accessory.enabled = false;
        }
    }

    public void ChangeExpression(int value)
    {
        expression = value;
    }
    public void ChangeExpression(int value, int returnValue, float duration)
    {
        expression = value;
        StartCoroutine(ReturnExpression(returnValue,duration));
    }
    IEnumerator ReturnExpression(int returnExpression,float value)
    {
        yield return new WaitForSeconds(value);
        ChangeExpression(returnExpression);
    }

    public void SetEyes(bool set)
    {
        eyesOn = set;
    }
    public void SetNose(bool set)
    {
        noseOn = set;
    }
    public void SetMouth(bool set)
    {
        mouthOn = set;
    }

    public void SetHeart(bool set)
    {
        heartOn = set;
    }

    public void SetSoul(bool set)
    {
        soulOn = set;
    }

    public void SetAccessory(bool set)
    {
        accessoryOn = set;
    }
}
