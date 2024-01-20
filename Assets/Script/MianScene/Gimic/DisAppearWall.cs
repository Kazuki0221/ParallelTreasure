using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通り抜けられる壁の処理を管理するクラス
/// </summary>
public class DisAppearWall : GroundColorChange
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーが接触したら自分を非表示にする
        if(CState != ColorState.Green && collision.CompareTag("Player"))
        {
            var sprite = GetComponent<SpriteRenderer>();
            sprite.enabled= false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //プレイヤーが離れたら自分を表示する

        if (CState != ColorState.Green && collision.CompareTag("Player"))
        {
            var sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = true;
        }
    }
}
