using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��DB�p�N���X
/// </summary>
[CreateAssetMenu]
[SerializeField]
public class TreasureDataBase : ScriptableObject
{
    public List<Treasure> treasures = new List<Treasure>();
}
