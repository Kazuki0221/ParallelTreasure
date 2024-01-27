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
    [SerializeField, Header("�f�[�^�E�B���h�E�����{�^��")] Button _closeDataWindowButton;

    //TreasreList�����{�^���i�[�p���X�g
    [SerializeField, Header("TreasreList�����{�^��")] Button _closeTreasreListButton;

    [SerializeField] Button _backButton;



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

        var fadeController = FindObjectOfType<FadeController>();
        StartCoroutine(fadeController.FadeOut(fadeController.FadeSpeed));
        AudioManager.instance.PlayBGM("Main");
    }

    /// <summary>
    /// �I�v�V�����E�B���h�E���J������
    /// </summary>
    public void OpenOptionWindow()
    {
        _optionWindow.SetActive(true);
    }

    /// <summary>
    /// �I�v�V�����E�B���h�E����鏈��
    /// </summary>
    public void CloseOptionWindow()
    {
        _optionWindow.SetActive(false);
    }

    /// <summary>
    /// �Z�[�u�f�[�^�p�E�B���h�E���J������
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
    /// �󕨃��X�g���J������
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
