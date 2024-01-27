using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �󕨃��X�g�̏������Ǘ�����N���X
/// </summary>
public class TreasureListView : MonoBehaviour
{
    [SerializeField]TreasureDataBase _treasureData;  //�󕨂�DB
    [SerializeField]GameObject contents = default;
    [SerializeField]GameObject imgPrefs;�@�@�@�@�@�@//�󕨉摜
    [SerializeField] Sprite notHave;                //�����Ă��Ȃ��󕨂�\������p�̉摜

    private void Awake()
    {
        try
        {
            //��DB�Ǝ莝���̕󕨂��r���āA�����Ă���󕨂݂̂����ꂼ��̉摜�ŕ\������
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
