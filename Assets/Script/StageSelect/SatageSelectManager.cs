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
    [SerializeField] List<Button> _stageList = new List<Button>();  //ステージ遷移ボタンのリスト

    //オプション関連
    [SerializeField, Header("オプションウィンドウ")] GameObject _optionWindow;
    [SerializeField] Button _optionButton;
    [SerializeField] Button[] _buttons = new Button[2];


    //データウィンドウ関連
    [SerializeField,Header("データウィンドウ")] GameObject _dataWindow;
    //ウィンドウを閉じるボタン格納用リスト
    [SerializeField] Button _closeDataWindowButton;


    //宝物リスト関連
    [SerializeField] GameObject treasureList;


    //TreasreListを閉じるボタン格納用リスト
    [SerializeField, Header("TreasreListを閉じるボタン")] Button _closeTreasreListButton;

    [SerializeField] Button _backButton;

    [SerializeField] FadeController _fadeController = null;


    void Start()
    {
        _stageList.ForEach(btn => btn.onClick.AddListener(() => StartCoroutine(GameManager.instance.ToNext(btn.name))));
        _closeDataWindowButton.onClick.AddListener(CloseOptionWindow);
        _closeTreasreListButton.onClick.AddListener(CloseTeasureList);

        _optionWindow.SetActive(false);
        _optionButton.onClick.AddListener(OpenOptionWindow);
        _buttons[0].onClick.AddListener(() => OpenDataWindow("Save"));
        _buttons[1].onClick.AddListener(() => OpenDataWindow("Load"));
        _backButton.onClick.AddListener(() => StartCoroutine(GameManager.instance.ToNext("Title")));

        _dataWindow.SetActive(false);

        treasureList.SetActive(false);

        GameManager.instance.saveFlg = false;
        GameManager.instance.loadFlg = false;

        _fadeController.FadeOut();
        AudioManager.instance.PlayBGM("Main");
    }

    /// <summary>
    /// オプションウィンドウを開く処理
    /// </summary>
    public void OpenOptionWindow()
    {
        _optionWindow.SetActive(true);
    }

    /// <summary>
    /// オプションウィンドウを閉じる処理
    /// </summary>
    public void CloseOptionWindow()
    {
        _optionWindow.SetActive(false);
    }

    /// <summary>
    /// セーブデータ用ウィンドウを開く処理
    /// </summary>
    /// <param name="flg"></param>
    public void OpenDataWindow(string flg)
    {
        if (flg == "Save")
        {
            GameManager.instance.saveFlg = true;
            GameManager.instance.loadFlg = false;
        }
        else if (flg == "Load")
        {
            GameManager.instance.saveFlg = false;
            GameManager.instance.loadFlg = true;
        }
        _dataWindow.SetActive(true);
    }

    /// <summary>
    /// 宝物リストを開く処理
    /// </summary>
    public void OpenTreasureList()
    {
        treasureList.SetActive(true);
    }
    
    public void CloseTeasureList()
    {
        treasureList.SetActive(false);
    }


}
