using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �F�̎��
/// </summary>
public enum ColorState
{
    Normal = 0,
    Red = 1,
    Blue = 2,
    Yellow = 3,
    Green = 4,
}


/// <summary>
/// �F�̏������Ǘ�����N���X
/// </summary>
public abstract class ColorController : MonoBehaviour
{

    [SerializeField] ColorState _colorState = ColorState.Normal;
    public ColorState CState
    {
        set
        {
            _colorState= value;
        }
        get { return _colorState; }
        
    }

    private void Update()
    {
        Action();
    }
    public abstract void Action();

}
