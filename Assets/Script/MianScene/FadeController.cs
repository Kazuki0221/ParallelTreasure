using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] Animation fadeIn;
    [SerializeField] Animation fadeOut;

    public void FadeIn()
    {
        fadeIn.Play();
    }

    public void FadeOut()
    {
        fadeOut.Play();
    }
}
