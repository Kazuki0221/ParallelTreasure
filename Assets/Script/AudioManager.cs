using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音声データを管理するクラス
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

    //ボリューム保存用のデフォルト値
    private const float _bgmVolumeDefault = 1.0f;
    private const float _seVolumeDefault = 1.0f;

    //BGMがフェードするのにかかる時間
    public const float _bgmFadeSpeedRateHigh = 0.9f;
    public const float _bgmFadeSpeedRateLow = 0.3f;
    private float _bgmFadeSpeedRate = _bgmFadeSpeedRateHigh;

    //BGMをフェードアウト中か
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

        //現在BGMが流れていない時はそのまま流す
        if (!_bgmSource.isPlaying)
        {
            _nextBGMName = "";
            _bgmSource.clip = _bgmDic[bgmName] as AudioClip;
            _bgmSource.Play();
        }
        //違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
        else if (_bgmSource.clip.name != bgmName)
        {
            _nextBGMName = bgmName;
            FadeOutBGM(fadeSpeedRate);
        }
    }

    /// <summary>
    /// BGMをすぐに止める
    /// </summary>
    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    /// <summary>
    /// 現在流れている曲をフェードアウトさせる
    /// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
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

        //徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
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
