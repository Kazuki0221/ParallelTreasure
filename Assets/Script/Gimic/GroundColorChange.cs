using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColorChange : ColorController
{
    [SerializeField] Color[] colors= null;

    public void ChangeColor(ColorState colorState)
    {
        CState = colorState;
        gameObject.GetComponent<SpriteRenderer>().color = colors[(int)CState];
    }

    public override void Action()
    {
    }
}
