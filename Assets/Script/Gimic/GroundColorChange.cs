using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColorChange : MonoBehaviour
{
    [SerializeField] Color[] colors= null;

    public void ChangeColor(int colorNum)
    {
        if (colorNum == 5) colorNum = 0;
        gameObject.GetComponent<SpriteRenderer>().color = colors[colorNum];
    }
}
