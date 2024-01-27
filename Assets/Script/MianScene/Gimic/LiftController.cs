using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���t�g�̏������Ǘ�����N���X
/// </summary>
public class LiftController : GroundColorChange
{
    [SerializeField] GameContoroller _gameContoroller;
    [SerializeField, Header("�ړ����x")] float _speed = 3f;
    [SerializeField, Header("�n�_�ʒu")] Transform _startPos = null;
    [SerializeField, Header("�I�_�ʒu")] Transform _endPos = null;
    float _moveTimer = 0f;
    float _stopTimer = 0f;
    [SerializeField] float _delayTime = 3f;

    bool _isArrival = false;
    bool _reveral = false;

    Vector3 _goalPosition = Vector2.zero;

    float _distance => Vector2.Distance(_startPos.position, _endPos.position);


    void Start()
    {
        _goalPosition = _endPos.position;
    }

    public override void Action()
    {
        if (CState == ColorState.Yellow && !_gameContoroller.IsChangeColor)
        {
            ChangeIsArrival();
            LiftMove(Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

    /// <summary>
    /// �ړI�n�ɓ��������Ƃ��Ɉ�莞�ԓ������~���鏈��
    /// </summary>
    public void ChangeIsArrival()
    {
        if (_isArrival)
        {
            if (_stopTimer >= _delayTime)
            {
                _isArrival = false;
                _stopTimer = 0f;
            }
            else if (_stopTimer < _delayTime)
            {
                _stopTimer += Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    /// <param name="deltaTime"></param>
    public void LiftMove(float deltaTime)
    {
        if (!_isArrival)
        {
            _moveTimer += deltaTime;
            float currentPosition = (_moveTimer * _speed) / _distance;

            if (!_reveral)
            {
                transform.position = Vector3.Lerp(_startPos.position, _endPos.position, currentPosition);
            }
            else
            {
                transform.position = Vector3.Lerp(_endPos.position, _startPos.position, currentPosition);
            }

            if (transform.position == _goalPosition)
            {
                _moveTimer = 0f;
                _reveral = !_reveral;
                ChangeGoalPosition();
                _isArrival = true;
            }
        }
    }

    /// <summary>
    /// �n�_�ƏI�_�����ւ��鏈��
    /// </summary>
    public void ChangeGoalPosition()
    {
        if (!_reveral)
        {
            _goalPosition = _endPos.position;
        }
        else
        {
            _goalPosition = _startPos.position;
        }
    }
}


