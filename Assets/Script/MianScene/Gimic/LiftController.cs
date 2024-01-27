using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リフトの処理を管理するクラス
/// </summary>
public class LiftController : GroundColorChange
{
    [SerializeField] GameContoroller _gameContoroller;
    [SerializeField, Header("移動速度")] float _speed = 3f;
    [SerializeField, Header("始点位置")] Transform _startPos = null;
    [SerializeField, Header("終点位置")] Transform _endPos = null;
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
    /// 目的地に到着したときに一定時間動きを停止する処理
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
    /// 移動処理
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
    /// 始点と終点を入れ替える処理
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


