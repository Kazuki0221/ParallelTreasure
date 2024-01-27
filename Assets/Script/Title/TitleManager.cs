using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �^�C�g����ʂ̏������Ǘ�����N���X
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

        //�Z�[�u�f�[�^�����݂��Ȃ���΃R���e�j���[�{�^���̔�����Ȃ���
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
    /// �V�K�f�[�^���쐬���鏈��
    /// </summary>
    /// <param name="input"></param>
    public void CreateData(TMP_InputField input)
    {
        GameManager.instance.SaveData = _saveView.CreateData(input.text, GameManager.instance.SaveData);
        StartCoroutine(GameManager.instance.ToNext("Tutorial"));
    }

    /// <summary>
    /// �f�[�^�쐬�p�E�B���h�E�̕\������
    /// </summary>
    public void ShowCreateWindow()
    {
        _createWindow.SetActive(true);
    }

    /// <summary>
    /// �f�[�^�E�B���h�E�̕\������
    /// </summary>
    public void OpenDataWindow()
    {
        GameManager.instance.loadFlg = true;
        _dataWindow.SetActive(true);
    }

}
