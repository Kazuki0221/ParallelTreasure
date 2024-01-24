using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// セーブデータウィンドウを管理するクラス
/// </summary>
public class SaveView : SaveManager
{
    public static SaveView instance;

    GameManager _gameManager;
    [SerializeField] Button[] _dataButtons = new Button[3];  //セーブデータボタンを格納する配列

    //クッションウィンドウ関連
    [SerializeField] GameObject _cushionWindow;              
    [SerializeField] Button[] _cushionButtons = new Button[2];　
    [SerializeField] Button _closeButton;

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
        _closeButton.onClick.AddListener(_gameManager.Close);
    }

    public void OnEnable()
    {
        if (!_gameManager.saveFlg && !_gameManager.loadFlg)
        {
            return;
        }
        else if (_gameManager.saveFlg)　　　//セーブ時の処理
        {
            for (int i = 0; i < _dataButtons.Length; i++)
            {
                if (_dataButtons[i].interactable == false)
                {
                    _dataButtons[i].interactable = true;

                }
                _dataButtons[i].onClick.AddListener(() => Save(i));
            }
        }
        else if (_gameManager.loadFlg)     //ロード時の処理
        {
            for (int i = 0; i < _dataButtons.Length; i++)
            {
                if(ExistData(i))
                {
                    _dataButtons[i].interactable = true;
                    _dataButtons[i].onClick.AddListener(() => Load(i));

                }
                else if(!ExistData(i))
                {
                    _dataButtons[i].interactable = false;
                }
            }
        }
    }

    /// <summary>
    /// セーブボタンを押下したときの処理
    /// </summary>
    /// <param name="saveNum"></param>
    public void Save(int saveNum)
    {
        filePath = GetDataPath(saveNum);
        if (File.Exists(filePath))
        {
            _cushionWindow.SetActive(true);
        }
        else if (!File.Exists(filePath))
        {
            Save(filePath, _gameManager.SaveData);
            Debug.Log("データをセーブしました。");
            _gameManager.Close();
        }
    }

    /// <summary>
    /// セーブデータの上書き処理
    /// </summary>
    public void UpDateSaveData()
    {
        Save(filePath, _gameManager.SaveData);
        Debug.Log("データを上書きしました");
        _gameManager.Close();
    }

    /// <summary>
    /// ロードボタンを押下したときの処理
    /// </summary>
    /// <param name="saveNum"></param>
    public void Load(int saveNum)
    {
        filePath = GetDataPath(saveNum);
        if (File.Exists(filePath))
        {
            Debug.Log("ロードしました");
            _gameManager.SaveData = Load(filePath, _gameManager.SaveData);
            StartCoroutine(_gameManager.ToNext("StageSelect"));

        }
        else if (!File.Exists(filePath))
        {
            Debug.Log("データが存在しません");

        }
    }

}
