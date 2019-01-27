using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimEvents : MonoBehaviour
{
    public Animator anim;

    public void SetInJumpLoopTrue()
    {
        anim.SetBool("InJumpLoop", true);
    }

    public void SetFinishJumpFlags()
    {
        anim.SetBool("FinishJump", false);
        anim.SetBool("AfterJumpFinish", true);
    }
}
