using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 黄色ドア(自動ドア)の処理を管理するクラス
/// </summary>
public class YellowDoor : YellowController
{
    [SerializeField] float _speed;
    bool isOpen = false;

    public override void Action()
    {
        //ドアが開くときの処理
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
        //ステージの状態が黄色かつプレイヤーが接触しているときに開く
        if (collision.CompareTag("Player") && CState == ColorState.Yellow)
        {
            isOpen = true;
        }
    }
}
