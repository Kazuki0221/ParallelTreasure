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
/// ゲーム全体の管理するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;   //シングルトン用変数
    public static SaveData _saveData;    //セーブデータ用変数

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
    /// シーンの遷移処理
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
    /// アプリを閉じる処理
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
