using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorController : MonoBehaviour
{
    GameContoroller _gameContoroller = null;

    public enum ColorState
    {
        Normal,
        Red,
        Blue,
        Yellow,
        Green,
    }

    [SerializeField] ColorState _colorState = ColorState.Normal;
    public ColorState CState
    {
        private set
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
