using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    GameContoroller _gameController;

    [SerializeField] Image fade;
    [SerializeField] float _fadeSpeed = 3.0f;

    [SerializeField] GameObject stageUI;
 
    [SerializeField] CinemachineVirtualCameraBase _camera;

    [SerializeField] PlayableDirector _playableDirector = default;
    
    void Start()
    {
        _gameController = GetComponent<GameContoroller>();
        _gameController._isPlay = false;
        stageUI.SetActive(false);
    }

    public void ChangeCamera()
    {
        _camera.Priority = 0;
        stageUI.SetActive(true);
    }

    public void StartGame()
    {
        _gameController._isPlay = true;
    }

    public void PlayTimeline()
    {
        _playableDirector.Play();
    }

    
}
