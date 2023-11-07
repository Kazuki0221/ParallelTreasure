using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveData
{
    public string userName; //ユーザー名
    public float progress; //進行度
    public List<Treasure> treasures; //所持している宝

    public SaveData(string _userName, float _progress = 0, List<Treasure> _treasures = null)
    {
        this.userName = _userName;
        this.progress = _progress;
        if (_treasures == null)
        {
            this.treasures = new List<Treasure>();
        }
        else
        {
            this.treasures = new List<Treasure>(_treasures);
        }
    }
}
