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

    SpriteRenderer _spriteRenderer => GetComponentInChildren<SpriteRenderer>();


    public override void Action()
    {
        Vector2 tempSize = _spriteRenderer.size;


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
                if (_spriteRenderer.size.y < range)
                {
                    _spriteRenderer.size += Vector2.up * speed * Time.deltaTime;
                    tempSize = _spriteRenderer.size;
                }
                else if (_spriteRenderer.size.y >= range)
                {
                    _spriteRenderer.size = new Vector2(3, range);
                    tempSize = _spriteRenderer.size;
                    isStop = true;
                }
            }
            else if (!isStrech)
            {
                if (_spriteRenderer.size.y > 2)
                {
                    _spriteRenderer.size -= Vector2.up * speed * Time.deltaTime;
                    tempSize = _spriteRenderer.size;
                }
                else if (_spriteRenderer.size.y <= 2)
                {
                    _spriteRenderer.size = new Vector2(3, 2);
                    tempSize = _spriteRenderer.size;
                    isStop = true;
                }
            }
        }
    }
}
