using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

/// <summary>
/// セーブ/ロードの処理を管理するクラス
/// </summary>
public class SaveManager : MonoBehaviour
{

    /// <summary>
    /// 新規データを作成する処理
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="_saveData"></param>
    /// <returns></returns>
    public SaveData CreateData(string userName, SaveData _saveData)
    {
        _saveData = new SaveData(userName);
        Debug.Log($"{_saveData.userName},{_saveData.progress}%,{_saveData.treasures.Count}");
        return _saveData;
    }

    /// <summary>
    /// セーブ処理
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="_saveData"></param>
    public void Save(string filePath, SaveData _saveData)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
            }
            Debug.Log("更新");
            try
            {
                //上書き
                string json = JsonUtility.ToJson(_saveData);
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine(json);
                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
        catch (FileNotFoundException)
        {
            try
            {
                Debug.Log("新規作成");
                //新規作成
                string json = JsonUtility.ToJson(_saveData);
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine(json);
                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }

    /// <summary>
    /// ロード処理
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="_saveData"></param>
    /// <returns></returns>
    public SaveData Load(string filePath, SaveData _saveData)
    {
        try
        {
            string json;
            //ロード
            using (StreamReader sr = new StreamReader(filePath))
            {
                json = sr.ReadToEnd();
            }
            _saveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log($"{_saveData.userName},{_saveData.progress}%,{_saveData.treasures.Count}");

        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        return _saveData;

    }

    /// <summary>
    /// セーブデータのパスを検索する関数
    /// </summary>
    /// <param name="saveDataNum"></param>
    /// <returns></returns>
    public String GetDataPath(int saveDataNum)
    {
        string filePath = Application.persistentDataPath + @"\Savedata" +saveDataNum + ".json";
        return filePath;
    }

    public bool ExistData(int saveDataNum)
    {
        string filePath = GetDataPath(saveDataNum);

        if (File.Exists(filePath))
        {
            return true;
        }
        else if (!File.Exists(filePath))
        {
            return false;
        }

        return false;
    }
}
