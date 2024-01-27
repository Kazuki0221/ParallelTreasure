using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Z�[�u�f�[�^�E�B���h�E���Ǘ�����N���X
/// </summary>
public class SaveView : SaveManager
{

    [SerializeField] Button[] _dataButtons = new Button[3];  //�Z�[�u�f�[�^�{�^�����i�[����z��

    //�N�b�V�����E�B���h�E�֘A
    [SerializeField] GameObject _cushionWindow;              
    [SerializeField] Button[] _cushionButtons = new Button[2];�@
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
        else if (GameManager.instance.saveFlg)�@�@�@//�Z�[�u���̏���
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
        else if (GameManager.instance.loadFlg)     //���[�h���̏���
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
    /// �Z�[�u�{�^�������������Ƃ��̏���
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
            Debug.Log("�f�[�^���Z�[�u���܂����B");
            CloseWindow();
        }
    }

    /// <summary>
    /// �Z�[�u�f�[�^�̏㏑������
    /// </summary>
    public void UpDateSaveData()
    {
        Save(filePath, GameManager.instance.SaveData);
        Debug.Log("�f�[�^���㏑�����܂���");
        _cushionWindow.SetActive(false);
        CloseWindow();
    }

    /// <summary>
    /// ���[�h�{�^�������������Ƃ��̏���
    /// </summary>
    /// <param name="saveNum"></param>
    public void Load(int saveNum)
    {
        filePath = GetDataPath(saveNum);
        Debug.Log(saveNum);
        if (File.Exists(filePath))
        {
            Debug.Log("���[�h���܂���");
            GameManager.instance.SaveData = Load(filePath, GameManager.instance.SaveData);
            StartCoroutine(GameManager.instance.ToNext("StageSelect"));

        }
        else if (!File.Exists(filePath))
        {
            Debug.Log("�f�[�^�����݂��܂���");

        }
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

}
