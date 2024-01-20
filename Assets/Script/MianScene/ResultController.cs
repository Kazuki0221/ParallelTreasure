using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// リザルト画面の処理を管理するクラス
/// </summary>
public class ResultController : MonoBehaviour
{
    GameContoroller _gameContoroller;
    GameManager _gameManager;

    //UI関連
    [SerializeField] GameObject _resultPanel;
    [SerializeField] GameObject _gameOverPanel;
    List<Treasure> treasures = new List<Treasure>();
    [SerializeField] Transform _imageList;
    [SerializeField] Transform _textList;
    [SerializeField] GameObject _image;
    [SerializeField] GameObject _text;

    [SerializeField] TextMeshProUGUI sumText;
    [SerializeField] TextMeshProUGUI runkText;

    SaveManager saveManager;
    int sum = 0;
    void OnEnable()
    {
        _gameContoroller = FindObjectOfType<GameContoroller>();
        _gameManager = FindObjectOfType<GameManager>();

        //クリア時の処理
        if (_gameContoroller._isClear)
        {
            _gameOverPanel.SetActive(false);

            foreach (var l in _gameContoroller.Treasures)
            {
                treasures.Add(l);
            }

            //入手した宝物の画像と価格を表示
            if (treasures != null)
            {
                treasures.ForEach(t =>
                {
                    var image = Instantiate(_image);
                    image.transform.parent = _imageList.transform;

                    var text = Instantiate(_text);
                    text.transform.parent = _textList.transform;
                    text.GetComponent<TextMeshProUGUI>().text = $"￥{t.price}00000";
                    sum += t.price;

                    if (!_gameManager.SaveData.treasures.Contains(t))
                    {
                        _gameManager.SaveData.treasures.Add(t);
                    }

                });
            }

            //宝物の総額の評価
            if(sum > 0)
            {
                sumText.text = $"合計：￥{sum}00000";
            }
            else if(sum <= 0)
            {
                sumText.text = $"合計：￥0";
            }

            if (sum >= 0 && sum < 1)
            {
                runkText.text = "あなたはDランクです";
            }
            else if(sum >= 1 && sum < 10)
            {
                runkText.text = "あなたはCランクです";
            }
            else if (sum >= 10 && sum < 100)
            {
                runkText.text = "あなたはBランクです";
            }
            else if (sum >= 100 && sum < 1000)
            {
                runkText.text = "あなたはAランクです";
            }
            else if (sum >= 1000)
            {
                runkText.text = "あなたはSランクです";
            }

        }
        else if (_gameContoroller._isGameOver)
        {
            _resultPanel.SetActive(false);
        }
    }

    /// <summary>
    /// ステージ選択画面への遷移処理
    /// </summary>
    public void ToStageSelect()
    {
       StartCoroutine(_gameManager.ToNext("StageSelect"));
    }


    /// <summary>
    /// リトライ処理
    /// </summary>
    public void Retry()
    {
        StartCoroutine(_gameManager.ToNext(SceneManager.GetActiveScene().name));
    }



}
