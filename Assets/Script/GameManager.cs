using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SaveManager
{
    public static string _userName;
    string filePath;
    SaveManager SaveManager;
    [SerializeField] GameObject _createWindow;
    [SerializeField] GameObject _saveWindow;
    [SerializeField] GameObject _loadWindow;
    [SerializeField] GameObject _cushionWindow;

    private void Start()
    {
        SaveManager= GetComponent<SaveManager>();
        _createWindow.SetActive(false);
        _saveWindow.SetActive(false);
        _cushionWindow.SetActive(false);
        _loadWindow.SetActive(false);
    }

    public void ToNext(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void CreateData(TMP_InputField input)
    {
        SaveManager.CreateData(input.text);
    }

    public void ShowCreateWindow()
    {
        _createWindow.SetActive(true);
    }
    public void ShowSaveWindow()
    {
        _saveWindow.SetActive(true);
    }

    public void ShowLoadWindow()
    {
        _loadWindow.SetActive(true);
    }

    public void Save(int saveNum)
    {
        filePath = SaveManager.GetDataPath(saveNum);
        if (File.Exists(filePath))
        {
            _cushionWindow.SetActive(true);
        }
        else if (!File.Exists(filePath))
        {
            SaveManager.Save(filePath);
            Debug.Log("データをセーブしました。");
            Close();
        }
        Debug.Log(filePath);
    }

    public void UpDateSaveData()
    {
        SaveManager.Save(filePath);
        Debug.Log("データを上書きしました");
        Close();
    }

    public void Load(int saveNum)
    {
        filePath = SaveManager.GetDataPath(saveNum);
        if (File.Exists(filePath))
        {
            Debug.Log("ロードしました");
            SaveManager.Load(filePath);
        }
        else if (!File.Exists(filePath))
        {
            Debug.Log("データが存在しません");

        }
    }

    public void Close()
    {
        GameObject[] window = GameObject.FindGameObjectsWithTag("Window");
        foreach(var w in window)
        {
            w.SetActive(false);
        }
    }
}
