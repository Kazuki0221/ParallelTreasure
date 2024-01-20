using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 壁の色変更処理を管理するクラス
/// </summary>
public class WallColorController : ColorController
{
    Animator animator => GetComponent<Animator>();

    private void OnEnable()
    {
        animator.SetInteger("ColorNum", (int)CState);
    }
    public override void Action()
    {
    }
}
