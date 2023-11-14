using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;



public class GameContoroller : MonoBehaviour
{
    bool _isChangeColor = false;

    [SerializeField] GameObject colorSelectButton;
    [SerializeField] TextMeshProUGUI durabilityText;
    [SerializeField] TextMeshProUGUI treasureCountText;

    public bool _isGameOver = false;
    public bool _isClear = false;
    public bool _isPlay = false;

    PlayerController player = null;
    List<ColorController> colorController = null;
    List<ColorController> tempObj = null;
    List<GroundColorChange> grounds = null;

    List<Treasure> _treasures = new List<Treasure> { };
    public List<Treasure> Treasures => _treasures;
    [SerializeField] GameObject _clearAnim = null;
    [SerializeField] GameObject _result = null;

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
        player = FindObjectOfType<PlayerController>();
        colorController = FindObjectsOfType<ColorController>().ToList();
        colorController.Where(c => !c.CompareTag("Ground")).ToList().ForEach(c => c.gameObject.SetActive(false));

        grounds = FindObjectsOfType<GroundColorChange>().ToList();

        colorSelectButton.SetActive(false);
        _isPlay = true;
        _clearAnim.SetActive(false);
        _result.SetActive(false);

    }

    void Update()
    {
        durabilityText.text = $"‘Ï‹v“xF{player.Durability}";

        treasureCountText.text = $"~{_treasures.Count}";

    }


    public void ActiveColorChanger()
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

    public void Judgement()
    {
        if (_isClear)
        {
            _clearAnim.SetActive(true);
        }
        else if (_isGameOver)
        {
            _result.SetActive(true);
        }
    }

    public void ChangeStageColor(ColorState colorState)
    {
        switch (colorState)
        {
            case ColorState.Red:
                {
                    var list = colorController.Where(c => c.CState == ColorState.Red).ToList();
                    list.ForEach(l => l.gameObject.SetActive(true));
                    if (tempObj != null)
                    {
                        tempObj.ForEach(o => o.gameObject.SetActive(false));
                    }

                    tempObj = list;
                }
                break;

            case ColorState.Blue:
                {
                    var list = colorController.Where(c => c.CState == ColorState.Blue).ToList();
                    list.ForEach(l => l.gameObject.SetActive(true));
                    if (tempObj != null)
                    {
                        tempObj.ForEach(o => o.gameObject.SetActive(false));
                    }

                    tempObj = list;
                }

                break;

            case ColorState.Yellow:
                {
                    var list = colorController.Where(c => c.CState == ColorState.Yellow).ToList();
                    list.ForEach(l => l.gameObject.SetActive(true));
                    if (tempObj != null)
                    {
                        tempObj.ForEach(o => o.gameObject.SetActive(false));
                    }

                    tempObj = list;
                }
                break;

            case ColorState.Green:
                {
                    var list = colorController.Where(c => c.CState == ColorState.Green).ToList();
                    list.ForEach(l => l.gameObject.SetActive(true));
                    if (tempObj != null)
                    {
                        tempObj.ForEach(o => o.gameObject.SetActive(false));
                    }

                    tempObj = list;
                }
                break;
            case ColorState.Normal:
                {
                    if (tempObj != null)
                    {
                        tempObj.ForEach(o => o.gameObject.SetActive(false));
                    }
                }
                break;
            default: return;
        }
        grounds.ForEach(g => g.ChangeColor(colorState));
        IsChangeColor = false;
        colorSelectButton.SetActive(false);
    }

    public void AddTreasure(GameObject treasure, GameObject image = null)
    {
        if (image != null)
        {
            image.SetActive(true);
        }
        _treasures.Add(treasure.GetComponent<TreasureController>().Treaure);
    }

    public void Clear()
    {
        _clearAnim.SetActive(false);
        _result.SetActive(true);
    }

    public bool ChackKey()
    {
        if (Input.anyKey)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(code))
                {
                    if(code != KeyCode.Alpha1 && code != KeyCode.Alpha2 && code != KeyCode.Alpha3 && code != KeyCode.Alpha4 && code != KeyCode.Alpha5)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
