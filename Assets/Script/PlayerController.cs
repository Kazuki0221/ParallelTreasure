using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField,Header("ˆÚ“®‘¬“x")] float _speed = 3.0f;
    [SerializeField,Header("HP")] float _durability = 5.0f;
    Rigidbody2D _rb2D= default;

    GameContoroller _gameContoroller = null;
    Vector2 tempVelocity= Vector2.zero;

    void Start()
    {
        _rb2D= GetComponent<Rigidbody2D>();
        _gameContoroller = FindObjectOfType<GameContoroller>();
    }

    void Update()
    {
        if (_gameContoroller._isPlay)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _gameContoroller.IsChangeColor = !_gameContoroller.IsChangeColor;
                tempVelocity = _rb2D.velocity;
                if (!_gameContoroller.IsChangeColor)
                {
                    _rb2D.velocity = tempVelocity;
                }
            }

            if (!_gameContoroller.IsChangeColor)
            {
                var horizontal = Input.GetAxisRaw("Horizontal");
                var moveDirction = new Vector2(horizontal, 0).normalized * _speed;
                float verticalVelocity = _rb2D.velocity.y;
                _rb2D.velocity = moveDirction + Vector2.up * verticalVelocity;
            }
            else
            {
                _rb2D.velocity = Vector2.zero;

                int colorFlg = 0;
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    colorFlg = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    colorFlg = 2;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    colorFlg = 3;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    colorFlg = 4;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    colorFlg = 5;
                }
                _gameContoroller.ChangeStageColor(colorFlg);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Thorn"))
        {
            _gameContoroller._isGameOver = true;
            _gameContoroller._isPlay = false;
        }
    }
}
