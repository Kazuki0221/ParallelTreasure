using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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

    public void CreateData(TMP_InputField input)
    {
        _gameManager.SaveData = _saveView.CreateData(input.text, _gameManager.SaveData);
        StartCoroutine(_gameManager.ToNext("StageSelect"));
    }

    public void ShowCreateWindow()
    {
        _createWindow.SetActive(true);
    }

    public void OpenDataWindow()
    {
        _gameManager.loadFlg = true;
        _dataWindow.SetActive(true);
    }

}
