using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトル画面の処理を管理するクラス
/// </summary>
public class TitleManager : MonoBehaviour
{
    SaveView _saveView;
    [SerializeField] Button continueButton = null;
    [SerializeField] GameObject _createWindow;

    [SerializeField] GameObject _dataWindow;


    private void Start()
    {
        _createWindow.SetActive(false);
        _dataWindow.SetActive(false);
        _saveView = _dataWindow.GetComponent<SaveView>();

        //セーブデータが存在しなければコンテニューボタンの判定をなくす
        if(!_saveView.ExistData(1) && !_saveView.ExistData(2) && !_saveView.ExistData(3))
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }

        GameManager.instance.loadFlg = false;

        var fadeController = FindObjectOfType<FadeController>();
        StartCoroutine(fadeController.FadeOut(fadeController.FadeSpeed));
        AudioManager.instance.PlayBGM("Main");
    }

    /// <summary>
    /// 新規データを作成する処理
    /// </summary>
    /// <param name="input"></param>
    public void CreateData(TMP_InputField input)
    {
        GameManager.instance.SaveData = _saveView.CreateData(input.text, GameManager.instance.SaveData);
        StartCoroutine(GameManager.instance.ToNext("Tutorial"));
    }

    /// <summary>
    /// データ作成用ウィンドウの表示処理
    /// </summary>
    public void ShowCreateWindow()
    {
        _createWindow.SetActive(true);
    }

    /// <summary>
    /// データウィンドウの表示処理
    /// </summary>
    public void OpenDataWindow()
    {
        GameManager.instance.loadFlg = true;
        _dataWindow.SetActive(true);
    }

}
