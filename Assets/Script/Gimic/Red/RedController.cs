using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedController : ColorController
{
    float damage = 0.5f;

    public override void Action()
    {
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController _playerController = collision.GetComponent<PlayerController>();

            _playerController.Hit(damage);
        }
    }
}
