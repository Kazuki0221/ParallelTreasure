using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowDoor : YellowController
{
    [SerializeField] float _speed;
    bool isOpen = false;

    public override void Action()
    {
        if (isOpen)
        {
            if(transform.localScale.y > 0)
            {
                transform.localScale -= Vector3.up * _speed * Time.deltaTime;
            }
            else if(transform.localScale.y <= 0)
            {
                transform.localScale = Vector3.zero;
                isOpen = false;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && CState == ColorState.Yellow)
        {
            isOpen = true;
        }
    }
}
