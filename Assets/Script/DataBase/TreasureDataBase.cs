using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宝物DB用クラス
/// </summary>
[CreateAssetMenu]
[SerializeField]
public class TreasureDataBase : ScriptableObject
{
    public List<Treasure> treasures = new List<Treasure>();
}
