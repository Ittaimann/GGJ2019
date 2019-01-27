using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : Pickup
{
    public GameDataScriptable gameDataScriptable;
    public GameObject broken, notBroken;

    public override void StartDay()
    {

        print("running?");
        if (gameDataScriptable.isBroken)
        {
            print("??");
            broken.SetActive(true);
            notBroken.SetActive(false);
        }
        else
        {

            print("here");
            broken.SetActive(false);
            notBroken.SetActive(true);
        }
    }
}
