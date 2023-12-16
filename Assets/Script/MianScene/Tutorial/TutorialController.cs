using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TutorialController : GameContoroller
{
    //GameContoroller _gameController;

    [SerializeField] GameObject stageUI;
 
    [SerializeField] CinemachineVirtualCameraBase _camera;

    [SerializeField] GameObject _tutorialAnim = default;
    
    public override void Start()
    {
        base.Start();
        stageUI.SetActive(false);
    }

    public void ChangeCamera()
    {
        _camera.Priority = 0;
        stageUI.SetActive(true);
    }

    public void StartGame()
    {
        _isPlay = true;
    }

    public override IEnumerator FadeOut()
    {
        yield return _fadeController.FadeOut(_fadeController.FadeSpeed);

        _tutorialAnim.GetComponent<PlayableDirector>().Play();

        yield return null;
    }

    
}
