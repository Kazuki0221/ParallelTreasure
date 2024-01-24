using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̏������Ǘ�����N���X
/// </summary>
public class PlayerController : MonoBehaviour
{

    /// <summary>
    /// �v���C���[�̏��
    /// </summary>
    enum State
    {
        Normal,
        Swim,
    }

    [SerializeField, Header("HP")] float _durability = 5.0f;
    public float Durability => _durability;
    [SerializeField,Header("�ړ����x")] float _speed = 3.0f;
    [SerializeField, Header("�W�����v��")] float _jumpPower = 3.0f;
    bool _invincible = false;
    public bool Invicible => _invincible;
    [SerializeField,Header("���G����")]float _invicibilityDuration = 5f;
    float _invincibilityTimer = 0;
    [SerializeField, Header("�_�Ŏ���")] float _cycle = 1;

    float _lastHorizontal = 0f;
    Rigidbody2D _rb2D= default;
    Vector2 tempVelocity = Vector2.zero;

    Animator _animator => GetComponent<Animator>();
    AnimationClip _tempAnim  = null;
    SpriteRenderer _sr => GetComponent<SpriteRenderer>();

    GameContoroller _gameContoroller = null;

    bool _isJump = true;

    //�o��̕ϐ�
    bool _isClimb = false;
    public float _distance;
    public LayerMask _ivyLayer;


    [SerializeField] State _state = State.Normal;


    float time;

    void Start()
    {
        _rb2D= GetComponent<Rigidbody2D>();
        _gameContoroller = FindObjectOfType<GameContoroller>();
        _state = State.Normal;
    }

    void Update()
    {
        //�|�[�Y���̏���
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

        //�_���[�W���̏���
        if (_invincible)
        {
            if(_invincibilityTimer <= _invicibilityDuration)
            {
                _invincibilityTimer += Time.deltaTime;

                var repeatValue = Mathf.Repeat(_invincibilityTimer, _cycle);
                SetAlpha(repeatValue >= _cycle * 0.5f ? 1 : 0);
            }
            else
            {
                _invincibilityTimer = 0;
                _invincible = false;
                //������Ԃɖ߂�
                SetAlpha();
                _sr.enabled = true;
                
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ǂƂ̐ڐG���̏���
        if (collision.gameObject.CompareTag("Thorn"))
        {
            _gameContoroller._isGameOver = true;
            _gameContoroller.Judgement();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //�n�ʂɐڐG���̏���
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //�n�ʂ𗣂ꂽ���̏���
        if (collision.gameObject.CompareTag("Ground") && _state == State.Normal)
        {
            _isJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�S�[�����̏���
        if (collision.gameObject.CompareTag("Goal"))
        {
            _gameContoroller._isClear = true;
            gameObject.SetActive(false);
            _gameContoroller.Judgement();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //���ɐڐG�������̏���
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
        //�����痣�ꂽ���̏���
        if (collision.gameObject.CompareTag("Water"))
        {
            _state = State.Normal;
            _rb2D.drag = 0;
            _isJump = false;
            _animator.SetBool("IsSwim", false);
        }
    }


    /// <summary>
    /// �v���C���[�̓������Ǘ�����֐�
    /// </summary>
    void Move()
    {
        if (_gameContoroller._isPlay)
        {
            //���V�t�g�L�[�������ƐF�̕ύX������
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

            //�F�ς������Ă��Ȃ��Ƃ��݈̂ړ����\
            if (!_gameContoroller.IsChangeColor)
            {
                _animator.speed = 1;
                var horizontal = Input.GetAxisRaw("Horizontal");
                float verticalVelocity = _rb2D.velocity.y;
                var moveDirection = new Vector2(horizontal, 0).normalized * _speed;

                _rb2D.velocity = moveDirection + Vector2.up * verticalVelocity;

                //�o���I�u�W�F�N�g�����邩�̔���
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
                    //�W�����v���̏���
                    if (Input.GetButtonDown("Jump") && _isJump)
                    {
                        _rb2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                    }
                    _rb2D.gravityScale = 1;
                }
                else if(_isClimb)
                {
                    //�o��Ƃ��̏���
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
                //�F��ύX����Ƃ��̏���
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

    /// <summary>
    /// �����]������
    /// </summary>
    /// <param name="horizontal"></param>
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

    /// <summary>
    /// �_���[�W���̏���
    /// </summary>
    /// <param name="damage"></param>
    public void Hit(float damage)
    {
        if (!_invincible)
        {
            if (_durability > 0)
            {
                _durability -= damage;
                _gameContoroller.OnDamage();
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

    /// <summary>
    /// ���x�㏸����
    /// </summary>
    /// <param name="speed"></param>
    public void AddSpeed(float speed)
    {
        _speed += speed;
    }

    /// <summary>
    /// ���x��������
    /// </summary>
    /// <param name="speed"></param>
    public void DelaySpeed(float speed)
    {
        var newSpeed = _speed - speed;
        _speed = newSpeed;
    }

    /// <summary>
    /// �W�����v�͏㏸����
    /// </summary>
    /// <param name="jumpPower"></param>
    public void AddJumpPower(float jumpPower)
    {
        _jumpPower += jumpPower;
    }

    /// <summary>
    /// �_�ŏ���
    /// </summary>
    /// <param name="alpha"></param>
    void SetAlpha(float alpha = 1)
    {
        var color = _sr.color;
        color.a = alpha;
        _sr.color = color;
    }
}
