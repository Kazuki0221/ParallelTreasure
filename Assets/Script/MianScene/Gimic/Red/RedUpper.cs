using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedUpper : RedController
{
    [SerializeField, Header("”­ŽË‘¬“x")] float speed = 3.0f;
    [SerializeField, Header("Œø‰Ê”ÍˆÍ")] float range = 5.0f;
    [SerializeField, Header("”­ŽËŠÔŠu")] float delay = 2f;

    float timer = 0f;
    bool isStrech = false;
    bool isStop = true;

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
}
