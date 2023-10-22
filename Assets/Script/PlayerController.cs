using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    bool _invincible = false;
    public bool Invicible => _invincible;
    [SerializeField,Header("–³“GŽžŠÔ")]float _invicibilityDuration = 5f;
    float _invincibilityTimer = 0;

    float _lastHorizontal = 0f;
    Rigidbody2D _rb2D= default;
    Animator _animator => GetComponent<Animator>();
    SpriteRenderer _sr => GetComponent<SpriteRenderer>();

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
            _rb2D.drag = 2;
        }
        Move();

        if (_invincible)
        {
            _animator.SetBool("Invicible", _invincible);
            if(_invincibilityTimer <= _invicibilityDuration)
            {
                _invincibilityTimer += Time.deltaTime;
            }
            else
            {
                _invincibilityTimer = 0;
                _invincible = false;
                _animator.SetBool("Invicible", _invincible);
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
            //_animator.SetFloat("Speed", 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            _gameContoroller._isClear = true;
            _gameContoroller._isPlay = false;
            gameObject.SetActive(false);
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

                if(_lastHorizontal != horizontal)
                {
                    ChangeDirection(horizontal);
                }

                _rb2D.velocity = moveDirction + Vector2.up * verticalVelocity;
                _animator.SetFloat("Speed", Mathf.Abs(_rb2D.velocity.x));

                if (Input.GetButtonDown("Jump") && _isJump)
                {
                    _rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                    //_animator.SetFloat("Speed", 0);
                }

                _lastHorizontal = horizontal;
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

    void ChangeDirection(float horizontal)
    {
        if(horizontal > 0)
        {
            _sr.flipX = false;
        }
        else if(horizontal < 0)
        {
            _sr.flipX = true;
        }
    }

    public void Hit(float damage)
    {
        if (!_invincible)
        {
            if (_durability > 0)
            {
                _durability -= damage;
            }
            else
            {
                _durability = 0;
            }
            _invincible = true;
        }
        else
        {
            return;
        }
    }

    public void AddSpeed(float speed)
    {
        _speed += speed;
    }
}
