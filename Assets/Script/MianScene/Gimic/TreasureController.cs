using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �󕨂̃^�C�v
/// </summary>
public enum TreasureType
{
    Normal = 0,
    Speed = 1,
    Jump = 2
}

/// <summary>
/// �󕨂̏������Ǘ�����N���X
/// </summary>
public class TreasureController : MonoBehaviour
{
    [SerializeField]Treasure treasure;
    public Treasure Treaure => treasure;

    [SerializeField]
    TreasureType treasureType;
    public TreasureType TreasureType => treasureType;

    [HideInInspector]
    public GameObject _speedBufImage = null;        //���x�o�t�p�̉摜

    [HideInInspector]
    public float _speed = 0;�@�@�@�@�@�@�@�@�@�@�@�@//���x�̃o�t�l

    [HideInInspector]
    public GameObject _jumpBufImage = null;�@�@�@�@//�W�����v�o�t�p�̉摜

    [HideInInspector]
    public float _jumpPower = 0;                   //�W�����v�̃o�t�l

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�ɐڐG����Ɣ�\���ɂ���
        if (collision.gameObject.CompareTag("Player"))
        {
            GameContoroller gameContoroller = FindObjectOfType<GameContoroller>();
            EffectOfTreasureType((int)TreasureType, collision, gameContoroller);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// �󕨂̌��ʏ���
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="collision"></param>
    /// <param name="gameContoroller"></param>
    void EffectOfTreasureType(int _type, Collider2D collision, GameContoroller gameContoroller)
    {
        switch (_type)
        {
            case 0:
                {
                    gameContoroller.AddTreasure(gameObject);
                    gameObject.SetActive(false);
                }
                break;
            case 1:
                {
                    gameContoroller.AddTreasure(gameObject, _speedBufImage);
                    PlayerController _playerController = collision.gameObject.GetComponent<PlayerController>();
                    _playerController.AddSpeed(_speed);
                }
                break;
            case 2:
                {
                    gameContoroller.AddTreasure(gameObject, _jumpBufImage);
                    PlayerController _playerController = collision.gameObject.GetComponent<PlayerController>();
                    _playerController.AddJumpPower(_jumpPower);
                }
                break;
            default:
                break;
        }
    }
}
