using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveData _saveData;
    public void CreateData(string userName)
    {
        _saveData = new SaveData(userName);
        Debug.Log($"{_saveData.userName},{_saveData.progress}%,{_saveData.treasures.Count}");
    }

    public void Save(string filePath)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
            }
            Debug.Log("�X�V");
            try
            {
                //�㏑��
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
                Debug.Log("�V�K�쐬");
                //�V�K�쐬
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

    public void Load(string filePath)
    {
        try
        {
            string json;
            //���[�h
            using (StreamReader sr = new StreamReader(filePath))
            {
                json = sr.ReadToEnd();
            }
            _saveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log($"{_saveData.userName},{_saveData.progress}%,{_saveData.treasures.Count}");

        }
        catch (FileNotFoundException)
        {
            try
            {
                //�f�[�^���Ȃ����Ƃ̕\��
                
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }

    public String GetDataPath(int saveDataNum)
    {
        string filePath = Application.persistentDataPath + @"\Savedata" +saveDataNum + ".json";
        return filePath;
    }
}
