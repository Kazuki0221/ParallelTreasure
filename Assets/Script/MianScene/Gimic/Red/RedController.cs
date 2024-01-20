using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 赤オブジェクトの処理を管理するクラス
/// </summary>
public class RedController : ColorController
{
    float damage = 0.5f;

    public override void Action()
    {
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        //プレイヤーが接触したときにダメージを与える
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController _playerController = collision.GetComponent<PlayerController>();

            _playerController.Hit(damage);
        }
    }
}
