using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : Pickup
{
    public GameDataScriptable gameDataScriptable;
    public GameObject broken, notBroken;

    public override void StartDay()
    {

        if (gameDataScriptable.isBroken)
        {
            broken.SetActive(true);
            notBroken.SetActive(false);
        }
        else
        {
            broken.SetActive(false);
            notBroken.SetActive(true);
        }
    }
}
