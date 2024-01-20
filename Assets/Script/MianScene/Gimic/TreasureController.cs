using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宝物のタイプ
/// </summary>
public enum TreasureType
{
    Normal = 0,
    Speed = 1,
    Jump = 2
}

/// <summary>
/// 宝物の処理を管理するクラス
/// </summary>
public class TreasureController : MonoBehaviour
{
    [SerializeField]Treasure treasure;
    public Treasure Treaure => treasure;

    [SerializeField]
    TreasureType treasureType;
    public TreasureType TreasureType => treasureType;

    [HideInInspector]
    public GameObject _speedBufImage = null;        //速度バフ用の画像

    [HideInInspector]
    public float _speed = 0;　　　　　　　　　　　　//速度のバフ値

    [HideInInspector]
    public GameObject _jumpBufImage = null;　　　　//ジャンプバフ用の画像

    [HideInInspector]
    public float _jumpPower = 0;                   //ジャンプのバフ値

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーに接触すると非表示にする
        if (collision.gameObject.CompareTag("Player"))
        {
            GameContoroller gameContoroller = FindObjectOfType<GameContoroller>();
            EffectOfTreasureType((int)TreasureType, collision, gameContoroller);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 宝物の効果処理
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
