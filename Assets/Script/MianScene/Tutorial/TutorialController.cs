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
    //GameContoroller _gameController;

    [SerializeField] GameObject stageUI;
 
    [SerializeField] CinemachineVirtualCameraBase _camera;  //変更後のカメラ

    [SerializeField] GameObject _tutorialAnim = default;
    
    public override void Start()
    {
        base.Start();
        stageUI.SetActive(false);
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
    }

    /// <summary>
    /// フェード処理
    /// </summary>
    /// <returns></returns>
    public override IEnumerator FadeOut()
    {
        yield return _fadeController.FadeOut(_fadeController.FadeSpeed);

        _tutorialAnim.GetComponent<PlayableDirector>().Play();

        yield return null;
    }

    
}
