using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

/// <summary>
/// �`���[�g���A���V�[�����Ǘ�����N���X
/// </summary>
public class TutorialController : GameContoroller
{
    [SerializeField] GameObject stageUI;
 
    [SerializeField] CinemachineVirtualCameraBase _camera;  //�ύX��̃J����

    [SerializeField] GameObject _tutorialAnim = default;
    
    public override void Start()
    {
        base.Start();
        stageUI.SetActive(false);
    }

    /// <summary>
    /// �J�����̕ύX����
    /// </summary>
    public void ChangeCamera()
    {
        _camera.Priority = 0;
        stageUI.SetActive(true);
    }

    /// <summary>
    /// �Q�[�����X�^�[�g���鏈��
    /// </summary>
    public void StartGame()
    {
        _isPlay = true;
    }

    /// <summary>
    /// �t�F�[�h����
    /// </summary>
    /// <returns></returns>
    public override IEnumerator FadeOut()
    {
        yield return _fadeController.FadeOut(_fadeController.FadeSpeed);

        _tutorialAnim.GetComponent<PlayableDirector>().Play();

        yield return null;
    }

    public void PlayAttentionSE()
    {
        AudioManager.instance.PlaySE("Attention");
    }

    public void PlayMoveWallSE()
    {
        AudioManager.instance.PlaySE("MoveWall");
    }

    public void PlayPushSwitchSE()
    {
        AudioManager.instance.PlaySE("PushSwitch");
    }
    
}
