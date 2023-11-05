using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class TreasureDataBase : ScriptableObject
{
    public List<Treasure> treasures = new List<Treasure>();
}
