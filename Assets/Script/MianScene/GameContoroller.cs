using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���C���V�[���ł̏������Ǘ�����N���X
/// </summary>
public class GameContoroller : MonoBehaviour
{
    bool _isChangeColor = false; //�F�ύX���ł��邩�ǂ���
    public bool _isGameOver = false; //�Q�[���I�[�o�[�t���O
    public bool _isClear = false; //�N���A�t���O
    public bool _isPlay = false; //�v���C�\��


    //UI�֘A
    [SerializeField] GameObject colorSelectButton;
    [SerializeField] Image[] durabilities = new Image[5];
    int durabilitiesIndex;
    [SerializeField, Header("�ϋv�x�̏��")] Sprite[] durabilitiesStates = new Sprite[3]; // 0: Full, 1:Half, 2:empty
    [SerializeField] TextMeshProUGUI treasureCountText;
    BackgroundView background = null;

    //�I�u�W�F�N�g�֘A
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
    /// �F�̕ύX�̉ۂ𔻒肷��ϐ�
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
        //�F���L�̃I�u�W�F�N�g�݂̂��i�[����(���Ɣw�i�ȊO��K�p)
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
    /// �F�̕ύX�̉ۂ̏���
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
    /// �N���A�E�Q�[���I�[�o�[����
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
    /// �F�̕ύX����
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
    /// �_���[�W���̑ϋv�x��UI����
    /// </summary>
    public void OnDamage()
    {
        if (durabilitiesIndex <= 0) return;
        //Debug.Log((float)durabilitiesIndex);
        //�����ʂ̏�Ԃɂ���āA�Ή�����摜�ɕύX����B
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
    /// �󕨂̓��莞�̏���
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
        treasureCountText.text = $"�~{_treasures.Count}";
    }

    /// <summary>
    /// �N���A���̏���
    /// </summary>
    public void Clear()
    {
        _clearAnim.SetActive(false);
        _result.SetActive(true);
    }

    /// <summary>
    /// �����ꂽ�L�[�̔��ʏ���
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
    /// �t�F�[�h����
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
