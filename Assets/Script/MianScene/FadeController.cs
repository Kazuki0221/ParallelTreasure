using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �t�F�[�h�������Ǘ�����N���X
/// </summary>
public class FadeController : MonoBehaviour
{
    Image fade => GetComponentInChildren<Image>(); //�t�F�[�h����摜

    [SerializeField]
     float _fadeSpeed;                 //�t�F�[�h���鑬�x

    public float FadeSpeed => _fadeSpeed;

    /// <summary>
    /// �t�F�[�h�C������
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
    /// �t�F�[�h�A�E�g����
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
