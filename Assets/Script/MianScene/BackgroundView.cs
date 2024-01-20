using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �w�i�摜�̏������Ǘ�����N���X
/// </summary>
public class BackgroundView : ColorController
{

    [SerializeField] Sprite[] backgrounds = new Sprite[5];

    /// <summary>
    /// �F�̕ύX��K�p���鏈��
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
