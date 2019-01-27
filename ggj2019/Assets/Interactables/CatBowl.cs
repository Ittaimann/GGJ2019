using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBowl : Interactable
{
    public GameDataScriptable gameDataScriptable;
    public GameObject catFood;

    public override void StartDay()
    {
        base.StartDay();
        print("resetr");
        gameDataScriptable.foodReady = false;
    }

    private void FixedUpdate()
    {
        catFood.SetActive(gameDataScriptable.foodReady);
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        base.Interact(heldObject, player);
        if(heldObject is CatFood)
        {
            catFood.SetActive(true);
            gameDataScriptable.foodReady = true;
        }
    }
}
