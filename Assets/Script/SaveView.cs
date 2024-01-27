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

    [SerializeField] Button[] _dataButtons = new Button[3];  //セーブデータボタンを格納する配列

    //クッションウィンドウ関連
    [SerializeField] GameObject _cushionWindow;              
    [SerializeField] Button[] _cushionButtons = new Button[2];　
    [SerializeField] Button _closeButton;

    string filePath;

    private void Start()
    {
        _cushionWindow.SetActive(false);
        _cushionButtons[0].onClick.AddListener(UpDateSaveData);
        _cushionButtons[1].onClick.AddListener(() => _cushionWindow.SetActive(false));
        _closeButton.onClick.AddListener(CloseWindow);
    }

    public void OnEnable()
    {
        if(GameManager.instance == null)
        {
            return;
        }

        if (!GameManager.instance.saveFlg && !GameManager.instance.loadFlg)
        {
            return;
        }
        else if (GameManager.instance.saveFlg)　　　//セーブ時の処理
        {
            for (int i = 0; i < _dataButtons.Length; i++)
            {
                var index = i + 1;

                if (_dataButtons[i].interactable == false)
                {
                    _dataButtons[i].interactable = true;

                }
                _dataButtons[i].onClick.AddListener(() => Save(index));
            }
        }
        else if (GameManager.instance.loadFlg)     //ロード時の処理
        {
            for (int i = 0; i < _dataButtons.Length; i++)
            {
                var index = i + 1;
                if(ExistData(index))
                {
                    _dataButtons[i].interactable = true;
                    _dataButtons[i].onClick.AddListener(() => Load(index));

                }
                else if(!ExistData(index))
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
            Save(filePath, GameManager.instance.SaveData);
            Debug.Log(filePath);
            Debug.Log("データをセーブしました。");
            CloseWindow();
        }
    }

    /// <summary>
    /// セーブデータの上書き処理
    /// </summary>
    public void UpDateSaveData()
    {
        Save(filePath, GameManager.instance.SaveData);
        Debug.Log("データを上書きしました");
        _cushionWindow.SetActive(false);
        CloseWindow();
    }

    /// <summary>
    /// ロードボタンを押下したときの処理
    /// </summary>
    /// <param name="saveNum"></param>
    public void Load(int saveNum)
    {
        filePath = GetDataPath(saveNum);
        Debug.Log(saveNum);
        if (File.Exists(filePath))
        {
            Debug.Log("ロードしました");
            GameManager.instance.SaveData = Load(filePath, GameManager.instance.SaveData);
            StartCoroutine(GameManager.instance.ToNext("StageSelect"));

        }
        else if (!File.Exists(filePath))
        {
            Debug.Log("データが存在しません");

        }
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

}
