using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedUpper : ColorController
{
    [SerializeField, Header("���ˑ��x")] float speed = 3.0f;
    [SerializeField, Header("���ʔ͈�")] float range = 5.0f;
    [SerializeField, Header("���ˊԊu")] float delay = 2f;

    float timer = 0f;
    bool isStrech = false;
    bool isStop = true;

    float damage = 0.5f;

    public override void Action()
    {
        Transform tempTransform = gameObject.transform;

        if (isStop)
        {
            if (timer < delay)
            {
                timer += Time.deltaTime;
            }
            else if (timer >= delay)
            {
                timer = 0;
                isStop = false;
                isStrech = !isStrech;
            }
        }
        else if (!isStop)
        {
            if (isStrech)
            {
                if (transform.localScale.y < range)
                {
                    transform.localScale += Vector3.up * speed * Time.deltaTime;
                    tempTransform.localScale = transform.localScale;
                }
                else if (transform.localScale.y >= range)
                {
                    transform.localScale = new Vector3(1, range, 1);
                    tempTransform.localScale = transform.localScale;
                    isStop = true;
                }
            }
            else if (!isStrech)
            {
                if (transform.localScale.y > 0)
                {
                    transform.localScale -= Vector3.up * speed * Time.deltaTime;
                    tempTransform.localScale = transform.localScale;
                }
                else if (transform.localScale.y <= 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    tempTransform.localScale = transform.localScale;
                    isStop = true;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController _playerController = collision.GetComponent<PlayerController>();

            _playerController.Hit(damage);
        }
    }

}
