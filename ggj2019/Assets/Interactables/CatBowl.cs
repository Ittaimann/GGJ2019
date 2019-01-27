using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBowl : Interactable
{
    public GameDataScriptable gameDataScriptable;

    public override void StartDay()
    {
        base.StartDay();
        print("resetr");
        gameDataScriptable.foodReady = false;
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        base.Interact(heldObject, player);
        if(heldObject is CatFood)
        {
            gameDataScriptable.foodReady = true;
        }
    }
}
