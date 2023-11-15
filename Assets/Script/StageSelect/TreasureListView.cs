using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TreasureListView : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField]TreasureDataBase _treasureData;
    [SerializeField]GameObject contents = default;
    [SerializeField]GameObject imgPrefs;
    [SerializeField] Sprite notHave;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();


        try
        {

            var haveTreasures = _gameManager.SaveData.treasures;

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
