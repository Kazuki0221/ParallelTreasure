using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ô‚ÌƒGƒŠƒA°
/// </summary>
public class RedArea : RedController
{
    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        //–Ø‚ğ”R‚â‚·
        if(collision.gameObject.CompareTag("Wood")){
            Destroy(collision.gameObject);
        }
    }
}
