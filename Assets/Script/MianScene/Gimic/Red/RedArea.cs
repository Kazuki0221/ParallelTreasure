using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ԃ̃G���A��
/// </summary>
public class RedArea : RedController
{
    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        //�؂�R�₷
        if(collision.gameObject.CompareTag("Wood")){
            Destroy(collision.gameObject);
        }
    }
}
