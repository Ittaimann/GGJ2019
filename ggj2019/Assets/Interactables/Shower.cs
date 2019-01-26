using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : Interactable
{
    public GameDataScriptable gameDataScriptable;

    // Start is called before the first frame update
    public override void StartDay()
    {
        base.StartDay();
        gameDataScriptable.tookShower = false;
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        gameDataScriptable.tookShower = true;
        this.enabled = false;
    }

}
