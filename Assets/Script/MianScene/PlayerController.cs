using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    enum State
    {
        Normal,
        Swim,
    }

    [SerializeField, Header("HP")] float _durability = 5.0f;
    public float Durability => _durability;
    [SerializeField,Header("移動速度")] float _speed = 3.0f;
    [SerializeField, Header("ジャンプ力")] float _jumpPower = 3.0f;
    bool _invincible = false;
    public bool Invicible => _invincible;
    [SerializeField,Header("無敵時間")]float _invicibilityDuration = 5f;
    float _invincibilityTimer = 0;

    float _lastHorizontal = 0f;
    Rigidbody2D _rb2D= default;
    Vector2 tempVelocity = Vector2.zero;

    Animator _animator => GetComponent<Animator>();
    AnimationClip _tempAnim  = null;
    SpriteRenderer _sr => GetComponent<SpriteRenderer>();

    GameContoroller _gameContoroller = null;

    bool _isJump = true;

    //登りの変数
    bool _isClimb = false;
    public float _distance;
    public LayerMask _ivyLayer;


    [SerializeField] State _state = State.Normal;

    void Start()
    {
        _rb2D= GetComponent<Rigidbody2D>();
        _gameContoroller = FindObjectOfType<GameContoroller>();
        Debug.Log(_gameContoroller.gameObject.name);
        _state = State.Normal;
    }

    void Update()
    {
        if (!_gameContoroller._isPlay)
        {
            _rb2D.velocity = Vector2.zero;
            _animator.speed = 0;
        }


        if(_durability <= 0)
        {
            _gameContoroller._isPlay = false;
            _gameContoroller._isGameOver = true;
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
            _gameContoroller.Judgement();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            _gameContoroller._isClear = true;
            gameObject.SetActive(false);
            _gameContoroller.Judgement();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            _state = State.Swim;
            _rb2D.drag = 2;
            _isJump = true;
            _animator.SetBool("IsSwim", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            _state = State.Normal;
            _rb2D.drag = 0;
            _isJump = false;
            _animator.SetBool("IsSwim", false);
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
                _gameContoroller.ActiveColorChanger();
            }

            if (!_gameContoroller.IsChangeColor)
            {
                _animator.speed = 1;
                var horizontal = Input.GetAxisRaw("Horizontal");
                float verticalVelocity = _rb2D.velocity.y;
                var moveDirection = new Vector2(horizontal, 0).normalized * _speed;

                _rb2D.velocity = moveDirection + Vector2.up * verticalVelocity;


                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 2, _ivyLayer);
                
                if(hitInfo.collider != null)
                {
                    if(Input.GetAxisRaw("Jump") > 0)
                    {
                        _isClimb = true;
                    }
                }
                else
                {
                    _isClimb = false;
                }

                if (!_isClimb)
                {

                    if (Input.GetButtonDown("Jump") && _isJump)
                    {
                        _rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                    }
                    _rb2D.gravityScale = 1;
                }
                else if(_isClimb)
                {
                    var _climb = Input.GetAxisRaw("Jump");
                    _rb2D.velocity = new Vector2(_rb2D.velocity.x, _climb * _speed);
                    _rb2D.gravityScale = 0;
                }

                _animator.SetFloat("Speed", Mathf.Abs(_rb2D.velocity.x));

                if (_lastHorizontal != horizontal)
                {
                    ChangeDirection(horizontal);
                }
                _lastHorizontal = horizontal;

            }
            else
            {
                _rb2D.velocity = Vector2.zero;
                _animator.speed = 0;

                if (_gameContoroller.ChackKey())
                {
                    ColorState cState = ColorState.Normal;
                    if (Input.GetKey(KeyCode.Alpha1))
                    {
                        cState = ColorState.Red;
                    }
                    else if (Input.GetKey(KeyCode.Alpha2))
                    {
                        cState = ColorState.Blue;
                    }
                    else if (Input.GetKey(KeyCode.Alpha3))
                    {
                        cState = ColorState.Yellow;
                    }
                    else if (Input.GetKey(KeyCode.Alpha4))
                    {
                        cState = ColorState.Green;
                    }
                    else if (Input.GetKey(KeyCode.Alpha5))
                    {
                        cState = ColorState.Normal;
                    }

                    _gameContoroller.ChangeStageColor(cState);
                   
                }
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

    public void DelaySpeed(float speed)
    {
        var newSpeed = _speed - speed;
        _speed = newSpeed;
    }
}
