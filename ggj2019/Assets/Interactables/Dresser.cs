using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dresser : Interactable
{
    public GameDataScriptable gameDataScriptable;

    // Start is called before the first frame update
    public override void StartDay()
    {
        base.StartDay();
        gameDataScriptable.gotDressed = false;
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        gameDataScriptable.gotDressed = true;
        this.enabled = false;
    }
}
