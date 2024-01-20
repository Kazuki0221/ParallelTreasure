using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェード処理を管理するクラス
/// </summary>
public class FadeController : MonoBehaviour
{

    Image fade => GetComponent<Image>(); //フェードする画像
    AudioSource audioSource;            //BGM

    [SerializeField]
     float _fadeSpeed;                 //フェードする速度

    public float FadeSpeed => _fadeSpeed;

    private void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeIn(float time)
    {
        var color = fade.color;

        var elapsed = 0f;

        while(elapsed < time)
        {
            elapsed += Time.deltaTime;
            color.a = elapsed / time;
            audioSource.volume = (float)(elapsed / time);
            fade.color = color;
            yield return null;
        }
        color.a = 1;
        audioSource.volume = 0;
        fade.color = color;
        yield return null;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeIn(_fadeSpeed));
    }


    /// <summary>
    /// フェードアウト処理
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeOut(float time)
    {
        var color = fade.color;

        var elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            color.a = 1 - elapsed / time;
            audioSource.volume = (float)(1 - elapsed / time);
            fade.color = color;
            yield return null;
        }

        color.a = 0;
        audioSource.volume = 1;
        fade.color = color;
        yield return null;
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut(_fadeSpeed));
    }

}
