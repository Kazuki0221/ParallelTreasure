using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// �X�e�[�W�I�����Ǘ�����N���X
/// </summary>
public class SatageSelectManager : MonoBehaviour
{
    GameManager _gameManager => FindObjectOfType<GameManager>();
    [SerializeField] List<Button> _stageList = new List<Button>();  //�X�e�[�W�J�ڃ{�^���̃��X�g

    //�I�v�V�����֘A
    [SerializeField] GameObject _optionWindow;
    [SerializeField] Button _optionButton;

    //�f�[�^�E�B���h�E�֘A
    [SerializeField] GameObject _dataWindow;
    [SerializeField] Button[] _buttons = new Button[2];

    //�󕨃��X�g�֘A
    [SerializeField] GameObject treasureList;

    //�E�B���h�E�����{�^���i�[�p���X�g
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
    /// �I�v�V�����E�B���h�E���J������
    /// </summary>
    public void OpenOptionWindow()
    {
        _optionWindow.SetActive(true);
    }

    /// <summary>
    /// �Z�[�u�f�[�^�p�E�B���h�E���J������
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
    /// �󕨃��X�g���J������
    /// </summary>
    public void ShowTreasureList()
    {
        treasureList.SetActive(true);
    }


}
