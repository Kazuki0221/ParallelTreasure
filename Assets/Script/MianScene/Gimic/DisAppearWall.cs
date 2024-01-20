using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ʂ蔲������ǂ̏������Ǘ�����N���X
/// </summary>
public class DisAppearWall : GroundColorChange
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[���ڐG�����玩�����\���ɂ���
        if(CState != ColorState.Green && collision.CompareTag("Player"))
        {
            var sprite = GetComponent<SpriteRenderer>();
            sprite.enabled= false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�v���C���[�����ꂽ�玩����\������

        if (CState != ColorState.Green && collision.CompareTag("Player"))
        {
            var sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = true;
        }
    }
}
