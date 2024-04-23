using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalGraphicManager : MonoBehaviour
{
    [Header("Eyes")]
    public SpriteRenderer eyes;
    bool eyesOn = true;
    public Sprite[] eyesSprites = new Sprite[5];

    [Header("Nose")]
    public SpriteRenderer nose;
    bool noseOn = true;
    public Sprite[] noseSprites = new Sprite[2];

    [Header("Mouth")]
    public SpriteRenderer mouth;
    bool mouthOn = true;
    public Sprite[] mouthSprites = new Sprite[5];

    public int expression = 0;    

    public void FixedUpdate()
    {
        FacialExpression();
    }

    void FacialExpression()
    {
        if (eyesOn) eyes.sprite = eyesSprites[expression];
        else eyes.sprite = eyesSprites[4];

        if (noseOn) nose.sprite = noseSprites[0];
        else nose.sprite = noseSprites[1];

        if (mouthOn) mouth.sprite = mouthSprites[expression];
        else mouth.sprite = mouthSprites[4];
    }

    public void ChangeExpression(int value)
    {
        expression = value;
    }

    public void EnableEyes(bool value)
    {
        eyes.enabled = value;
        eyesOn = value;
    }

    public void EnableNose(bool value)
    {
        nose.enabled = value;
        noseOn = value;
    }

    public void EnableMouth(bool value)
    {
        mouth.enabled = value;
        mouthOn = value;
    }
}
