using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class GameContoroller : MonoBehaviour
{
    enum StageState
    {
        Normal,
        Red,
        Blue,
        Yellow,
        Green,
    }

    StageState _state = StageState.Normal;

    StageState State
    {
        set { _state = value; }
        get { return _state; }
    }
    bool _isChangeColor = false;

    [SerializeField] GameObject colorSelectButton;
    [SerializeField] TextMeshProUGUI textMeshPro;

    public bool _isGameOver = false;
    public bool _isClear = false;
    public bool _isPlay = true;

    PlayerController player = null;
    public bool IsChangeColor
    {
        set
        {
            _isChangeColor = value;
        }
        get
        {
            return _isChangeColor;
        }
    }
   

    void Start()
    {
        player= FindAnyObjectByType<PlayerController>();
        colorSelectButton.SetActive(false);
        _isPlay = true;

    }

    void Update()
    {
        textMeshPro.text = $"ëœãvìxÅF{player.Durability}";

        if (_isPlay)
        {
            if (IsChangeColor)
            {
                colorSelectButton.SetActive(true);
            }
            else if (!IsChangeColor)
            {
                colorSelectButton.SetActive(false);
            }
        }
        else
        {
            if (_isClear)
            {
                Debug.Log("Clear");
            }
            else if (_isGameOver)
            {
                Debug.Log("GameOver");
            }
        }
        
    }

    public void ChangeStageColor(int colorNum)
    {
        switch (colorNum)
        {
            case 1: State = StageState.Red; 
                break;

            case 2: State = StageState.Blue;
                break;

            case 3: State = StageState.Yellow;
                break;

            case 4: State = StageState.Green;
                break;
            default: return;
        }
        IsChangeColor = false;
        colorSelectButton.SetActive(false);
    }
}
