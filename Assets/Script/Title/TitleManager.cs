using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TitleManager : MonoBehaviour
{
    GameManager _gameManager => FindObjectOfType<GameManager>();
    [SerializeField] GameObject _createWindow;

    [SerializeField] GameObject _dataWindow;


    private void Start()
    {
        _createWindow.SetActive(false);
        _dataWindow.SetActive(false);

        _gameManager.loadFlg = false;
    }

    public void CreateData(TMP_InputField input)
    {
        _gameManager._saveManager.CreateData(input.text);
        _gameManager.ToNext("StageSelect");
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
