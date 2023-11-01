using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColorChange : ColorController
{
    [SerializeField] Color[] colors= null;

    //public void ChangeColor(ColorState colorState)
    //{
    //    CState = colorState;
    //    gameObject.GetComponent<SpriteRenderer>().color = colors[(int)CState];
    //}

    public void ChangeColor(int colorState)
    {
        if (colorState == 5) colorState = 0;
        //CState = ;
        gameObject.GetComponent<SpriteRenderer>().color = colors[colorState];
    }


    public override void Action()
    {
    }
}
