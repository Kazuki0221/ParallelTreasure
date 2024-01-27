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
    BackgroundView background = null;

    //オブジェクト関連
    PlayerController player = null;
    List<ColorController> colorController = null;
    
    List<GroundColorChange> grounds = null;
    List<YellowDoor> doors = null;

    List<Treasure> _treasures = new List<Treasure> { };
    public List<Treasure> Treasures => _treasures;
    [SerializeField] GameObject _clearAnim = null;
    [SerializeField] GameObject _result = null;

    protected FadeController _fadeController = null;

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
        _fadeController = FindObjectOfType<FadeController>();
        StartCoroutine(FadeOut());
        AudioManager.instance.PlayBGM("Main");

        player = FindObjectOfType<PlayerController>();
        //色特有のオブジェクトのみを格納する(床と背景以外を適用)
        colorController = FindObjectsOfType<ColorController>().Where(c => !c.GetComponent<GroundColorChange>() && !c.CompareTag("Background") && !c.GetComponent<YellowDoor>()).ToList();
        colorController.Where(c => c.CState != ColorState.Normal).ToList().ForEach(c => c.gameObject.SetActive(false));

        background = FindObjectOfType<BackgroundView>();
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
        switch (colorState)
        {
            case ColorState.Red:
                {
                    var list = colorController.Where(c => c.CState == ColorState.Red).ToList();
                    list.ForEach(l => l.gameObject.SetActive(true));

                    var list2 = colorController.Where(c => c.CState != ColorState.Red).ToList();
                    list2.ForEach(l => l.gameObject.SetActive(false));
                }
                break;

            case ColorState.Blue:
                {
                    var list = colorController.Where(c => c.CState == ColorState.Blue).ToList();
                    list.ForEach(l => l.gameObject.SetActive(true));

                    var list2 = colorController.Where(c => c.CState != ColorState.Blue).ToList();
                    list2.ForEach(l => l.gameObject.SetActive(false));
                }

                break;

            case ColorState.Yellow:
                {
                    var list = colorController.Where(c => c.CState == ColorState.Yellow).ToList();
                    list.ForEach(l => l.gameObject.SetActive(true));

                    var list2 = colorController.Where(c => c.CState != ColorState.Yellow).ToList();
                    list2.ForEach(l => l.gameObject.SetActive(false));
                }
                break;

            case ColorState.Green:
                {
                    var list = colorController.Where(c => c.CState == ColorState.Green).ToList();
                    list.ForEach(l => l.gameObject.SetActive(true));

                    var list2 = colorController.Where(c => c.CState != ColorState.Green).ToList();
                    list2.ForEach(l => l.gameObject.SetActive(false));
                }
                break;
            case ColorState.Normal:
                {
                    var list = colorController.Where(c => c.CState == ColorState.Normal).ToList();
                    list.ForEach(l => l.gameObject.SetActive(true));

                    var list2 = colorController.Where(c => c.CState != ColorState.Normal).ToList();
                    list2.ForEach(l => l.gameObject.SetActive(false));
                }
                break;
            default: return;
        }
        background.ChangeColor(colorState);
        grounds.ForEach(g => g.ChangeColor(colorState));
        doors.ForEach(l => l.CState = colorState);
        IsChangeColor = false;
        colorSelectButton.SetActive(false);
    }

    /// <summary>
    /// ダメージ時の耐久度のUI処理
    /// </summary>
    public void OnDamage()
    {
        if (durabilitiesIndex <= 0) return;
        //Debug.Log((float)durabilitiesIndex);
        //減少量の状態によって、対応する画像に変更する。
        if ((float)durabilitiesIndex == player.Durability)
        {
            durabilities[durabilitiesIndex].sprite = durabilitiesStates[2];
            durabilitiesIndex--;
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

    /// <summary>
    /// フェード処理
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator FadeOut()
    {
        var fadeController = FindObjectOfType<FadeController>();

        yield return fadeController.FadeOut(fadeController.FadeSpeed);
        _isPlay = true;
        yield return null;
    }
}
