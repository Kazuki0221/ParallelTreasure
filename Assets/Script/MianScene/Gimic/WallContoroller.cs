using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ûôï«ÇÃèàóùÇä«óùÇ∑ÇÈÉNÉâÉX
/// </summary>
public class WallContoroller : MonoBehaviour
{
    [SerializeField, Header("à⁄ìÆë¨ìx")] float _speed = 3.0f;
    Rigidbody2D _rb2D = default;
    GameContoroller _gameContoroller = null;
    Vector2 moveDirction = Vector2.zero;

    public enum WallType
    {
        RightToLeft,
        BottomToTop
    }
    public WallType type = WallType.RightToLeft;

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
                if (type == WallType.RightToLeft)
                {
                    moveDirction = Vector2.right * _speed;
                }
                else if(type == WallType.BottomToTop)
                {
                    moveDirction = Vector2.up * _speed;
                }
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
