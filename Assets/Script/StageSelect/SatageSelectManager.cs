using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ステージ選択を管理するクラス
/// </summary>
public class SatageSelectManager : MonoBehaviour
{
    GameManager _gameManager => FindObjectOfType<GameManager>();
    [SerializeField] List<Button> _stageList = new List<Button>();  //ステージ遷移ボタンのリスト

    //オプション関連
    [SerializeField] GameObject _optionWindow;
    [SerializeField] Button _optionButton;

    //データウィンドウ関連
    [SerializeField] GameObject _dataWindow;
    [SerializeField] Button[] _buttons = new Button[2];

    //宝物リスト関連
    [SerializeField] GameObject treasureList;

    //ウィンドウを閉じるボタン格納用リスト
    [SerializeField] List<Button> _closeButtons= new List<Button>();


    void Awake()
    {
        _stageList.ForEach(btn => btn.onClick.AddListener(() => StartCoroutine(_gameManager.ToNext(btn.name))));
        _closeButtons.ForEach(btn => btn.onClick.AddListener(() => _gameManager.Close()));

        _optionWindow.SetActive(false);
        _optionButton.onClick.AddListener(OpenOptionWindow);
        _buttons[0].onClick.AddListener(() => OpenDataWindow("Save"));
        _buttons[1].onClick.AddListener(() => OpenDataWindow("Load"));

        _dataWindow = FindObjectOfType<SaveView>().gameObject;

        _dataWindow.SetActive(false);

        treasureList.SetActive(false);

        _gameManager.saveFlg = false;
        _gameManager.loadFlg = false;

        var fadeController = FindObjectOfType<FadeController>();
        StartCoroutine(fadeController.FadeOut(fadeController.FadeSpeed));
    }

    /// <summary>
    /// オプションウィンドウを開く処理
    /// </summary>
    public void OpenOptionWindow()
    {
        _optionWindow.SetActive(true);
    }

    /// <summary>
    /// セーブデータ用ウィンドウを開く処理
    /// </summary>
    /// <param name="flg"></param>
    public void OpenDataWindow(string flg)
    {
        if (flg == "Save")
        {
            _gameManager.saveFlg = true;
            _gameManager.loadFlg = false;
        }
        else if (flg == "Load")
        {
            _gameManager.saveFlg = false;
            _gameManager.loadFlg = true;
        }
        _dataWindow.SetActive(true);
    }

    /// <summary>
    /// 宝物リストを開く処理
    /// </summary>
    public void ShowTreasureList()
    {
        treasureList.SetActive(true);
    }


}
