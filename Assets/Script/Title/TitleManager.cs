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
    GameManager _gameManager => FindObjectOfType<GameManager>();
    SaveView _saveView;
    [SerializeField] GameObject _createWindow;

    [SerializeField] GameObject _dataWindow;


    private void Start()
    {
        _createWindow.SetActive(false);
        _dataWindow.SetActive(false);
        _saveView = _dataWindow.GetComponent<SaveView>();

        _gameManager.loadFlg = false;

        var fadeController = FindObjectOfType<FadeController>();
        StartCoroutine(fadeController.FadeOut(fadeController.FadeSpeed));
    }

    /// <summary>
    /// 新規データを作成する処理
    /// </summary>
    /// <param name="input"></param>
    public void CreateData(TMP_InputField input)
    {
        _gameManager.SaveData = _saveView.CreateData(input.text, _gameManager.SaveData);
        StartCoroutine(_gameManager.ToNext("Tutorial"));
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
        _gameManager.loadFlg = true;
        _dataWindow.SetActive(true);
    }

}
