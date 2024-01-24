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
    public static SaveView instance;

    GameManager _gameManager;
    [SerializeField] Button[] _dataButtons = new Button[3];  //�Z�[�u�f�[�^�{�^�����i�[����z��

    //�N�b�V�����E�B���h�E�֘A
    [SerializeField] GameObject _cushionWindow;              
    [SerializeField] Button[] _cushionButtons = new Button[2];�@
    [SerializeField] Button _closeButton;

    string filePath;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _gameManager = FindObjectOfType<GameManager>();
        _cushionWindow.SetActive(false);
        _cushionButtons[0].onClick.AddListener(UpDateSaveData);
        _cushionButtons[1].onClick.AddListener(_gameManager.Close);
        _closeButton.onClick.AddListener(_gameManager.Close);
    }

    public void OnEnable()
    {
        if (!_gameManager.saveFlg && !_gameManager.loadFlg)
        {
            return;
        }
        else if (_gameManager.saveFlg)�@�@�@//�Z�[�u���̏���
        {
            for (int i = 0; i < _dataButtons.Length; i++)
            {
                if (_dataButtons[i].interactable == false)
                {
                    _dataButtons[i].interactable = true;

                }
                _dataButtons[i].onClick.AddListener(() => Save(i));
            }
        }
        else if (_gameManager.loadFlg)     //���[�h���̏���
        {
            for (int i = 0; i < _dataButtons.Length; i++)
            {
                if(ExistData(i))
                {
                    _dataButtons[i].interactable = true;
                    _dataButtons[i].onClick.AddListener(() => Load(i));

                }
                else if(!ExistData(i))
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
            Save(filePath, _gameManager.SaveData);
            Debug.Log("�f�[�^���Z�[�u���܂����B");
            _gameManager.Close();
        }
    }

    /// <summary>
    /// �Z�[�u�f�[�^�̏㏑������
    /// </summary>
    public void UpDateSaveData()
    {
        Save(filePath, _gameManager.SaveData);
        Debug.Log("�f�[�^���㏑�����܂���");
        _gameManager.Close();
    }

    /// <summary>
    /// ���[�h�{�^�������������Ƃ��̏���
    /// </summary>
    /// <param name="saveNum"></param>
    public void Load(int saveNum)
    {
        filePath = GetDataPath(saveNum);
        if (File.Exists(filePath))
        {
            Debug.Log("���[�h���܂���");
            _gameManager.SaveData = Load(filePath, _gameManager.SaveData);
            StartCoroutine(_gameManager.ToNext("StageSelect"));

        }
        else if (!File.Exists(filePath))
        {
            Debug.Log("�f�[�^�����݂��܂���");

        }
    }

}
