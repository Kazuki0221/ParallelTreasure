using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundView : ColorController
{

    [SerializeField] Sprite[] backgrounds = new Sprite[5];

    public void ChangeColor(ColorState colorState)
    {
        CState = colorState;
        gameObject.GetComponent<SpriteRenderer>().sprite = backgrounds[(int)CState];
    }

    public override void Action()
    {
    }

}
