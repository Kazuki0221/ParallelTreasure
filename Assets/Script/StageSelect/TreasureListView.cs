using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 宝物リストの処理を管理するクラス
/// </summary>
public class TreasureListView : MonoBehaviour
{
    [SerializeField]TreasureDataBase _treasureData;  //宝物のDB
    [SerializeField]GameObject contents = default;
    [SerializeField]GameObject imgPrefs;　　　　　　//宝物画像
    [SerializeField] Sprite notHave;                //持っていない宝物を表示する用の画像

    private void Awake()
    {
        try
        {
            //宝物DBと手持ちの宝物を比較して、持っている宝物のみをそれぞれの画像で表示する
            var haveTreasures = GameManager.instance.SaveData.treasures;

            for (int i = 0; i < _treasureData.treasures.Count; i++)
            {
                var img = Instantiate(imgPrefs).GetComponent<Image>();
                img.transform.parent = contents.transform;
                var slot = img.transform.GetChild(0).GetComponent<Image>();
                if (haveTreasures.Count > 0 && haveTreasures.Contains(_treasureData.treasures[i]))
                {
                    slot.sprite = _treasureData.treasures[i].sprite;
                }
                else
                {
                    slot.sprite = notHave;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

}
