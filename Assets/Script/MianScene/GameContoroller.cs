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
    [SerializeField]BackgroundView background = null;

    //�I�u�W�F�N�g�֘A
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
        StartCoroutine(FadeOut());
        AudioManager.instance.PlayBGM("Main");

        //�F���L�̃I�u�W�F�N�g�݂̂��i�[����(���Ɣw�i�ȊO��K�p)
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
    /// �_���[�W���̑ϋv�x��UI����
    /// </summary>
    public void OnDamage()
    {
        if (durabilitiesIndex < 0) return;
        //�����ʂ̏�Ԃɂ���āA�Ή�����摜�ɕύX����B
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
    /// �t�F�[�h����
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator FadeOut()
    {
        yield return _fadeController.FadeOut(_fadeController.FadeSpeed);
        _isPlay = true;
        yield return null;
    }
}
