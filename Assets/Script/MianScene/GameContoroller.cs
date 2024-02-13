using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// メインシーンでの処理を管理するクラス
/// </summary>
public class GameContoroller : MonoBehaviour
{
    bool _isChangeColor = false; //色変更ができるかどうか
    public bool _isGameOver = false; //ゲームオーバーフラグ
    public bool _isClear = false; //クリアフラグ
    public bool _isPlay = false; //プレイ可能か


    //UI関連
    [SerializeField] GameObject colorSelectButton;
    [SerializeField] Image[] durabilities = new Image[5];
    int durabilitiesIndex;
    [SerializeField, Header("耐久度の状態")] Sprite[] durabilitiesStates = new Sprite[3]; // 0: Full, 1:Half, 2:empty
    [SerializeField] TextMeshProUGUI treasureCountText;
    [SerializeField]BackgroundView background = null;

    //オブジェクト関連
    [SerializeField]PlayerController player = null;
    List<ColorController> colorControllers = null;
    
    List<GroundColorChange> grounds = null;
    List<YellowDoor> doors = null;

    List<Treasure> _treasures = new List<Treasure> { };
    public List<Treasure> Treasures => _treasures;
    [SerializeField] GameObject _clearAnim = null;
    [SerializeField] GameObject _result = null;

    [SerializeField] protected FadeController _fadeController = null;

    /// <summary>
    /// 色の変更の可否を判定する変数
    /// </summary>
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

    public virtual void Start()
    {
        StartCoroutine(FadeOut());
        AudioManager.instance.PlayBGM("Main");

        //色特有のオブジェクトのみを格納する(床と背景以外を適用)
        colorControllers = FindObjectsOfType<ColorController>().Where(c => !c.GetComponent<GroundColorChange>() && !c.CompareTag("Background") && !c.GetComponent<YellowDoor>()).ToList();
        colorControllers.Where(c => c.CState != ColorState.Normal).ToList().ForEach(c => c.gameObject.SetActive(false));

        grounds = FindObjectsOfType<GroundColorChange>().ToList();
        doors = FindObjectsOfType<YellowDoor>().ToList();

        durabilitiesIndex = durabilities.Length - 1;
        colorSelectButton.SetActive(false);
        _clearAnim.SetActive(false);
        _result.SetActive(false);

    }

    /// <summary>
    /// 色の変更の可否の処理
    /// </summary>
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

    /// <summary>
    /// クリア・ゲームオーバー判定
    /// </summary>
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

        _isPlay = false;
    }


    /// <summary>
    /// 色の変更処理
    /// </summary>
    /// <param name="colorState"></param>
    public void ChangeStageColor(ColorState colorState)
    {
        foreach(ColorController colorController in colorControllers)
        {
            if(colorController.CState == colorState)
            {
                colorController.gameObject.SetActive(true);
            }
            else if(colorController.CState != colorState)
            {
                colorController.gameObject.SetActive(false);
            }
        }

        background.ChangeColor(colorState);
        grounds.ForEach(ground => ground.ChangeColor(colorState));
        doors.ForEach(door => door.CState = colorState);
        IsChangeColor = false;
        colorSelectButton.SetActive(false);
    }

    /// <summary>
    /// ダメージ時の耐久度のUI処理
    /// </summary>
    public void OnDamage()
    {
        if (durabilitiesIndex < 0) return;
        //減少量の状態によって、対応する画像に変更する。
        if ((float)durabilitiesIndex == player.Durability)
        {
            durabilities[durabilitiesIndex].sprite = durabilitiesStates[2];
            durabilitiesIndex--;
            Debug.Log($"Index:{durabilitiesIndex}");
        }
        else if ((float)durabilitiesIndex < player.Durability)
        {
            durabilities[durabilitiesIndex].sprite = durabilitiesStates[1];
        }
    }

    /// <summary>
    /// 宝物の入手時の処理
    /// </summary>
    /// <param name="treasure"></param>
    /// <param name="image"></param>
    public void AddTreasure(GameObject treasure, GameObject image = null)
    {
        if (image != null)
        {
            image.SetActive(true);
        }
        _treasures.Add(treasure.GetComponent<TreasureController>().Treaure);
        treasureCountText.text = $"×{_treasures.Count}";
    }

    /// <summary>
    /// クリア時の処理
    /// </summary>
    public void Clear()
    {
        _clearAnim.SetActive(false);
        _result.SetActive(true);
    }

    /// <summary>
    /// 押されたキーの判別処理
    /// </summary>
    /// <returns></returns>
    public bool CheckKey()
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

    /// <summary>
    /// フェード処理
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator FadeOut()
    {
        yield return _fadeController.FadeOut(_fadeController.FadeSpeed);
        _isPlay = true;
        yield return null;
    }
}
