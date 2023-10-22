using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTreasure : Treasure
{
    [SerializeField] float _speed;
    [SerializeField] GameObject _image = null;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameContoroller gameContoroller = FindObjectOfType<GameContoroller>();
            gameContoroller.AddTreasure(gameObject, _image);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController _playerController = collision.gameObject.GetComponent<PlayerController>();
            _playerController.AddSpeed(_speed);
        }
    }
}
