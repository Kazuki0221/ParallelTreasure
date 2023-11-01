using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : GroundColorChange
{

    [SerializeField] float _speed = 3f;
    [SerializeField] Transform _startPos = null;
    [SerializeField] Transform _endPos = null;
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
        if (CState == ColorState.Yellow)
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


