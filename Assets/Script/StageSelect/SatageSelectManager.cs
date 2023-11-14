using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SatageSelectManager : MonoBehaviour
{
    GameManager _gameManager => FindObjectOfType<GameManager>();
    [SerializeField] List<Button> _stageList = new List<Button>();

    [SerializeField] GameObject _optionWindow;
    [SerializeField] Button _optionButton;
    [SerializeField] GameObject _dataWindow;
    [SerializeField] Button[] _buttons = new Button[2];


    void Awake()
    {
        _stageList.ForEach(btn => btn.onClick.AddListener(() => _gameManager.ToNext(btn.name)));

        _optionWindow.SetActive(false);
        _optionButton.onClick.AddListener(OpenWindow);
        _buttons[0].onClick.AddListener(() => OpenDataWindow("Save"));
        _buttons[1].onClick.AddListener(() => OpenDataWindow("Load"));

        _dataWindow = FindObjectOfType<SaveView>().gameObject;

        _dataWindow.SetActive(false);

        _gameManager.saveFlg = false;
        _gameManager.loadFlg = false;
    }


    public void OpenWindow()
    {
        _optionWindow.SetActive(true);
    }

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


}
