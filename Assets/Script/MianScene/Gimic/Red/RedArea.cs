using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedArea : RedController
{
    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        if(collision.gameObject.CompareTag("Wood")){
            Destroy(collision.gameObject);
        }
    }
}
