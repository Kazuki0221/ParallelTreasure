using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景画像の処理を管理するクラス
/// </summary>
public class BackgroundView : ColorController
{

    [SerializeField] Sprite[] backgrounds = new Sprite[5];

    /// <summary>
    /// 色の変更を適用する処理
    /// </summary>
    /// <param name="colorState"></param>
    public void ChangeColor(ColorState colorState)
    {
        CState = colorState;
        gameObject.GetComponent<SpriteRenderer>().sprite = backgrounds[(int)CState];
    }

    public override void Action()
    {
    }

}
