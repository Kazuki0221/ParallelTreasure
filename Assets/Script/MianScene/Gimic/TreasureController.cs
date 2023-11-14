using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour
{
    [SerializeField]Treasure treasure;
    public Treasure Treaure => treasure; 

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameContoroller gameContoroller = FindObjectOfType<GameContoroller>();
            gameContoroller.AddTreasure(gameObject);
            gameObject.SetActive(false);
        }
    }

    //void EffectOfTreasureType(int _type, Collision2D collision)
    //{
    //    switch (_type)
    //    {
    //        case 0:
    //            {
    //                GameContoroller gameContoroller = FindObjectOfType<GameContoroller>();
    //                gameContoroller.AddTreasure(gameObject);
    //                gameObject.SetActive(false);
    //            }
    //            break;
    //        case 1:
    //            {
    //                GameContoroller gameContoroller = FindObjectOfType<GameContoroller>();
    //                gameContoroller.AddTreasure(gameObject, _image);
    //                gameObject.SetActive(false);
    //                PlayerController _playerController = collision.gameObject.GetComponent<PlayerController>();
    //                _playerController.AddSpeed(_speed);
    //            }
    //    }
    //}
}
