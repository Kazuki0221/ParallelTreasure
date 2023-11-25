using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisAppearWall : GroundColorChange
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(CState != ColorState.Green && collision.CompareTag("Player"))
        {
            var sprite = GetComponent<SpriteRenderer>();
            sprite.enabled= false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(CState != ColorState.Green && collision.CompareTag("Player"))
        {
            var sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = true;
        }
    }
}
