using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̐F�ύX���Ǘ�����N���X
/// </summary>
public class GroundColorChange : ColorController
{
    [SerializeField] Sprite[] sprites= null;
    Animator animator => GetComponent<Animator>();

    public void ChangeColor(ColorState colorState)
    {
        CState = colorState;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[(int)CState];
        animator.SetInteger("ColorNum",(int)CState);
    }

    public override void Action()
    {
    }
}
