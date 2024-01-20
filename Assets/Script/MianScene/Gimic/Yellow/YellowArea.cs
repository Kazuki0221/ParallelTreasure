using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 黄色のエリア床の処理を管理するクラス
/// </summary>
public class YellowArea : YellowController
{
    [SerializeField] float _delaySpeed = 3.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーが接触したときに減速させる
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
        //プレイヤーが離れたときに速度を元に戻す

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
