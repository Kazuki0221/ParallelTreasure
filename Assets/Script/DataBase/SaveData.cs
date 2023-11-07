using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveData
{
    public string userName; //���[�U�[��
    public float progress; //�i�s�x
    public List<Treasure> treasures; //�������Ă����

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
