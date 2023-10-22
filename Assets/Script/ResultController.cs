using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    GameContoroller _gameContoroller;
    [SerializeField] GameObject _resultPanel;
    [SerializeField] GameObject _gameOverText;
    List<int> treasures = new List<int>();
    [SerializeField] Transform _imageList;
    [SerializeField] Transform _textList;
    [SerializeField] GameObject _image;
    [SerializeField] GameObject _text;

    [SerializeField] TextMeshProUGUI sumText;
    [SerializeField] TextMeshProUGUI runkText;
    int sum = 0;
    void OnEnable()
    {
        _gameContoroller = FindObjectOfType<GameContoroller>();
        if (_gameContoroller._isClear)
        {
            _gameOverText.SetActive(false);

            foreach (var l in _gameContoroller.Treasures)
            {
                treasures.Add(l);
            }

            if (treasures != null)
            {
                treasures.ForEach(t =>
                {
                    var image = Instantiate(_image);
                    image.transform.parent = _imageList.transform;

                    var text = Instantiate(_text);
                    text.transform.parent = _textList.transform;
                    text.GetComponent<TextMeshProUGUI>().text = $"￥{t}00000";
                    sum += t;
                });
            }

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



}
