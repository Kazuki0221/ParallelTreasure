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
    Image fade => GetComponentInChildren<Image>(); //フェードする画像

    [SerializeField]
     float _fadeSpeed;                 //フェードする速度

    public float FadeSpeed => _fadeSpeed;

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
            fade.color = color;
            yield return null;
        }
        color.a = 1;
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
            fade.color = color;
            yield return null;
        }

        color.a = 0;
        fade.color = color;
        yield return null;
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut(_fadeSpeed));
    }

}
