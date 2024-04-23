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

    public int expression = 0;    

    public void FixedUpdate()
    {
        FacialExpression();
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
    }

    public void ChangeExpression(int value)
    {
        expression = value;
    }
}
