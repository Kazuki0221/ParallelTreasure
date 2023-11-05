using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowArea : YellowController
{
    [SerializeField] float _delaySpeed = 3.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.DelaySpeed(_delaySpeed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.AddSpeed(_delaySpeed);
            }
        }
    }
}
