using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宝物のデータを管理するクラス
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
