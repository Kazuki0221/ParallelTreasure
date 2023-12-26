using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColorController : ColorController
{
    Animator animator => GetComponent<Animator>();

    private void OnEnable()
    {
        animator.SetInteger("ColorNum", (int)CState);
    }
    public override void Action()
    {
    }
}
