using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorState
{
    Normal = 0,
    Red = 1,
    Blue = 2,
    Yellow = 3,
    Green = 4,
}

public abstract class ColorController : MonoBehaviour
{
    GameContoroller _gameContoroller = null;


    [SerializeField] ColorState _colorState = ColorState.Normal;
    public ColorState CState
    {
        set
        {
            _colorState= value;
        }
        get { return _colorState; }
        
    }
    void Start()
    {
        _gameContoroller = FindObjectOfType<GameContoroller>();
        
    }

    private void Update()
    {
        Action();
    }
    public abstract void Action();

}
