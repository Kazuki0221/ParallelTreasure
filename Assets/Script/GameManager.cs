using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static SaveData _saveData;

    public SaveData SaveData
    {
        set { _saveData = value; }
        get { return _saveData; }
    }

    public bool saveFlg = false;
    public bool loadFlg = false;

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
    }

    public void ToNext(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void Close()
    {
        GameObject[] window = GameObject.FindGameObjectsWithTag("Window");
        foreach(var w in window)
        {
            w.SetActive(false);
        }
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
