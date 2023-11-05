using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class Treasure : ScriptableObject
{
    public int id;
    public int price;
    //public int type;
    public Sprite sprite;
}
