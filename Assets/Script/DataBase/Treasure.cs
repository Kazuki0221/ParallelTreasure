using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �󕨂̃f�[�^���Ǘ�����N���X
/// </summary>
[CreateAssetMenu]
[SerializeField]
public class Treasure : ScriptableObject
{
    public int id;
    public int price;
    //public int type;
    public Sprite sprite;
}
