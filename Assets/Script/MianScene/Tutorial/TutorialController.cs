using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

/// <summary>
/// チュートリアルシーンを管理するクラス
/// </summary>
public class TutorialController : GameContoroller
{
    [SerializeField] GameObject stageUI;
 
    [SerializeField] CinemachineVirtualCameraBase _camera;  //変更後のカメラ

    [SerializeField] GameObject _tutorialAnim = default;

    public static bool isSkip = false;
    
    public override void Start()
    {
        base.Start();
        stageUI.SetActive(false);

        if(isSkip == true)
        {
            ChangeCamera();
        }
    }

    /// <summary>
    /// カメラの変更処理
    /// </summary>
    public void ChangeCamera()
    {
        _camera.Priority = 0;
        stageUI.SetActive(true);
    }

    /// <summary>
    /// ゲームをスタートする処理
    /// </summary>
    public void StartGame()
    {
        _isPlay = true;
        if(isSkip == false)
        {
            isSkip = true;
        }
    }

    /// <summary>
    /// フェード処理
    /// </summary>
    /// <returns></returns>
    public override IEnumerator FadeOut()
    {
        yield return _fadeController.FadeOut(_fadeController.FadeSpeed);

        if (isSkip == true)
        {
            StartGame();
        }
        else if(isSkip == false)
        {
            _tutorialAnim.GetComponent<PlayableDirector>().Play();
        }

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
