using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{

    Image fade => GetComponent<Image>();

    [SerializeField]
     float _fadeSpeed;

    public float FadeSpeed => _fadeSpeed;


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
