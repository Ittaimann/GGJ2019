using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : Interactable
{
    GameDataScriptable gameDataScriptable;
    private MeshRenderer broken, notBroken;

    public override void StartDay()
    {
        base.StartDay();
        if(gameDataScriptable.isBroken)
        {
            broken.enabled = true;
            notBroken.enabled = false;
        }
        else
        {
            broken.enabled = false;
            notBroken.enabled = true;
        }
    }
}
