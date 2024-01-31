using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �Q�[���S�̂̊Ǘ�����N���X
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;   //�V���O���g���p�ϐ�
    public static SaveData _saveData;    //�Z�[�u�f�[�^�p�ϐ�

    public SaveData SaveData
    {
        set { _saveData = value; }
        get { return _saveData; }
    }

    public bool saveFlg = false;
    public bool loadFlg = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    /// <summary>
    /// �V�[���̑J�ڏ���
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public IEnumerator ToNext(string sceneName)
    {
        var fadeController = FindObjectOfType<FadeController>();
        StartCoroutine(fadeController.FadeIn(fadeController.FadeSpeed));

        yield return fadeController.FadeIn(fadeController.FadeSpeed);
        SceneManager.LoadScene(sceneName);
        yield return null;
    }

    /// <summary>
    /// �A�v������鏈��
    /// </summary>
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
