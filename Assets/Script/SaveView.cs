using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveView : SaveManager
{
    public static SaveView instance;

    GameManager _gameManager;
    [SerializeField] Button[] _dataButtons = new Button[3];
    [SerializeField] GameObject _cushionWindow;
    [SerializeField] Button[] _cushionButtons = new Button[2];

    string filePath;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _gameManager = FindObjectOfType<GameManager>();
        _cushionWindow.SetActive(false);
        _cushionButtons[0].onClick.AddListener(UpDateSaveData);
        _cushionButtons[1].onClick.AddListener(_gameManager.Close);
    }

    public void OnEnable()
    {
        if (!_gameManager.saveFlg && !_gameManager.loadFlg)
        {
            return;
        }
        else if (_gameManager.saveFlg)
        {
            for (int i = 0; i < _dataButtons.Length; i++)
            {
                _dataButtons[i].onClick.AddListener(() => Save(i));
            }
        }
        else if (_gameManager.loadFlg)
        {
            for (int i = 0; i < _dataButtons.Length; i++)
            {
                _dataButtons[i].onClick.AddListener(() => Load(i));
            }
        }
    }

    public void Save(int saveNum)
    {
        filePath = GetDataPath(saveNum);
        if (File.Exists(filePath))
        {
            _cushionWindow.SetActive(true);
        }
        else if (!File.Exists(filePath))
        {
            Save(filePath);
            Debug.Log("データをセーブしました。");
            _gameManager.Close();
        }
    }

    public void UpDateSaveData()
    {
        Save(filePath);
        Debug.Log("データを上書きしました");
        _gameManager.Close();
    }

    public void Load(int saveNum)
    {
        filePath = GetDataPath(saveNum);
        if (File.Exists(filePath))
        {
            Debug.Log("ロードしました");
            Load(filePath);
        }
        else if (!File.Exists(filePath))
        {
            Debug.Log("データが存在しません");

        }
    }

}
