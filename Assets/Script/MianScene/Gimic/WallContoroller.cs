using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 棘壁の処理を管理するクラス
/// </summary>
public class WallContoroller : MonoBehaviour
{
    [SerializeField, Header("移動速度")] float _speed = 3.0f;
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
        else
        {
            _rb2D.velocity = Vector2.zero;
        }

    }
}
