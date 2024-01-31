using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����f�[�^���Ǘ�����N���X
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource _bgmSource;
    [SerializeField]
    AudioClip[] _bgmClips;

    List<AudioSource> _seSourceList;
    [SerializeField]
    AudioClip[] _seClips;

    const int _seSourceNum = 4;

    Dictionary<string, AudioClip> _bgmDic, _seDic;

    string _nextBGMName;
    string _nextSEName;

    //�{�����[���ۑ��p�̃f�t�H���g�l
    private const float _bgmVolumeDefault = 1.0f;
    private const float _seVolumeDefault = 1.0f;

    //BGM���t�F�[�h����̂ɂ����鎞��
    public const float _bgmFadeSpeedRateHigh = 0.9f;
    public const float _bgmFadeSpeedRateLow = 0.3f;
    private float _bgmFadeSpeedRate = _bgmFadeSpeedRateHigh;

    //BGM���t�F�[�h�A�E�g����
    private bool _isFadeOut = false;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameObject.AddComponent<AudioListener>();
        for(int i = 0; i < _seSourceNum + 1; i++) 
        { 
            gameObject.AddComponent<AudioSource>();
        }

        AudioSource[] audioSources = GetComponents<AudioSource>();
        _seSourceList= new List<AudioSource>();

        for(int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].playOnAwake = false;

            if(i == 0)
            {
                audioSources[i].loop = true;
                _bgmSource = audioSources[i];
                _bgmSource.volume = _bgmVolumeDefault;
            }
            else
            {
                _seSourceList.Add(audioSources[i]);
                audioSources[i].volume = _seVolumeDefault;
            }
        }

        _bgmDic= new Dictionary<string, AudioClip>();
        _seDic = new Dictionary<string, AudioClip>();


        foreach(AudioClip bgm in _bgmClips)
        {
            _bgmDic[bgm.name] = bgm;
        }
        foreach(AudioClip se in _seClips)
        {
            _seDic[se.name] = se;
        }
    }

    public void PlaySE(string seName)
    {
        if (!_seDic.ContainsKey(seName))
        {
            return;
        }

        _nextSEName= seName;
    }

    public void PlayBGM(string bgmName, float fadeSpeedRate = _bgmFadeSpeedRateHigh)
    {
        if (!_bgmDic.ContainsKey(bgmName))
        {
            return;
        }

        //����BGM������Ă��Ȃ����͂��̂܂ܗ���
        if (!_bgmSource.isPlaying)
        {
            _nextBGMName = "";
            _bgmSource.clip = _bgmDic[bgmName] as AudioClip;
            _bgmSource.Play();
        }
        //�ႤBGM������Ă��鎞�́A����Ă���BGM���t�F�[�h�A�E�g�����Ă��玟�𗬂��B����BGM������Ă��鎞�̓X���[
        else if (_bgmSource.clip.name != bgmName)
        {
            _nextBGMName = bgmName;
            FadeOutBGM(fadeSpeedRate);
        }
    }

    /// <summary>
    /// BGM�������Ɏ~�߂�
    /// </summary>
    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    /// <summary>
    /// ���ݗ���Ă���Ȃ��t�F�[�h�A�E�g������
    /// fadeSpeedRate�Ɏw�肵�������Ńt�F�[�h�A�E�g����X�s�[�h���ς��
    /// </summary>
    public void FadeOutBGM(float fadeSpeedRate = _bgmFadeSpeedRateLow)
    {
        _bgmFadeSpeedRate = fadeSpeedRate;
        _isFadeOut = true;
    }

    void Update()
    {
        if (!_isFadeOut)
        {
            return;
        }

        //���X�Ƀ{�����[���������Ă����A�{�����[����0�ɂȂ�����{�����[����߂����̋Ȃ𗬂�
        _bgmSource.volume -= Time.deltaTime * _bgmFadeSpeedRate;
        if (_bgmSource.volume <= 0)
        {
            _bgmSource.Stop();
            _bgmSource.volume = _bgmVolumeDefault;
            _isFadeOut = false;

            if (!string.IsNullOrEmpty(_nextBGMName))
            {
                PlayBGM(_nextBGMName);
            }
        }
    }

}
