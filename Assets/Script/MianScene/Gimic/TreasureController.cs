using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TreasureType
{
    Normal = 0,
    Speed = 1,
    Jump = 2
}
public class TreasureController : MonoBehaviour
{
    [SerializeField]Treasure treasure;
    public Treasure Treaure => treasure;

    [SerializeField]
    TreasureType treasureType;
    public TreasureType TreasureType => treasureType;

    [HideInInspector]
    public GameObject _speedBufImage = null;

    [HideInInspector]
    public float _speed = 0;

    [HideInInspector]
    public GameObject _jumpBufImage = null;

    [HideInInspector]
    public float _jumpPower = 0;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameContoroller gameContoroller = FindObjectOfType<GameContoroller>();
            EffectOfTreasureType((int)TreasureType, collision, gameContoroller);
            gameObject.SetActive(false);
        }
    }

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
