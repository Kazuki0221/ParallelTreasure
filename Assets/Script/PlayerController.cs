using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum State
    {
        Normal,
        Swim
    }

    [SerializeField, Header("HP")] float _durability = 5.0f;
    public float Durability => _durability;
    [SerializeField,Header("ˆÚ“®‘¬“x")] float _speed = 3.0f;
    [SerializeField, Header("ƒWƒƒƒ“ƒv—Í")] float _jumpPower = 3.0f;
    Rigidbody2D _rb2D= default;

    GameContoroller _gameContoroller = null;
    Vector2 tempVelocity= Vector2.zero;
    bool _isJump = true;
    [SerializeField] State _state = State.Normal;

    void Start()
    {
        _rb2D= GetComponent<Rigidbody2D>();
        _gameContoroller = FindObjectOfType<GameContoroller>();
        _state = State.Normal;
    }

    void Update()
    {
        if(_durability <= 0)
        {
            _gameContoroller._isPlay = false;
            _gameContoroller._isGameOver = true;
        }

        if(_state == State.Normal)
        {
            _rb2D.drag = 0;
        }
        else if(_state == State.Swim)
        {
            _rb2D.drag = 3;
        }
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Thorn"))
        {
            _gameContoroller._isGameOver = true;
            _gameContoroller._isPlay = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _state == State.Normal)
        {
            _isJump = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            _state = State.Swim;
            _isJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            _state = State.Normal;
            _isJump = false;
        }
    }

    void Move()
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

                if (Input.GetButtonDown("Jump") && _isJump)
                {
                    _rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                }
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
}
