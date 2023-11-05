using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallContoroller : MonoBehaviour
{
    [SerializeField, Header("ˆÚ“®‘¬“x")] float _speed = 3.0f;
    Rigidbody2D _rb2D = default;
    GameContoroller _gameContoroller = null;



    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _gameContoroller = FindObjectOfType<GameContoroller>();
    }

    void Update()
    {
        if (_gameContoroller._isPlay)
        {
            if (!_gameContoroller.IsChangeColor)
            {
                var moveDirction = Vector2.right * _speed;
                _rb2D.velocity = moveDirction;
            }
            else
            {
                _rb2D.velocity = Vector2.zero;
            }
        }

    }
}
