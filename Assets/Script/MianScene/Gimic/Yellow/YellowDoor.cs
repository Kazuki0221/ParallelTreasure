using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���F�h�A(�����h�A)�̏������Ǘ�����N���X
/// </summary>
public class YellowDoor : YellowController
{
    [SerializeField] float _speed;
    bool isOpen = false;

    public override void Action()
    {
        //�h�A���J���Ƃ��̏���
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
        //�X�e�[�W�̏�Ԃ����F���v���C���[���ڐG���Ă���Ƃ��ɊJ��
        if (collision.CompareTag("Player") && CState == ColorState.Yellow)
        {
            isOpen = true;
        }
    }
}
