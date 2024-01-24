using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

/// <summary>
/// �Z�[�u/���[�h�̏������Ǘ�����N���X
/// </summary>
public class SaveManager : MonoBehaviour
{

    /// <summary>
    /// �V�K�f�[�^���쐬���鏈��
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
    /// �Z�[�u����
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

    /// <summary>
    /// ���[�h����
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="_saveData"></param>
    /// <returns></returns>
    public SaveData Load(string filePath, SaveData _saveData)
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
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        return _saveData;

    }

    /// <summary>
    /// �Z�[�u�f�[�^�̃p�X����������֐�
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
