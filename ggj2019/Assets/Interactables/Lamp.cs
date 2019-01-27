using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Interactable
{
    public GameDataScriptable gameDataScriptable;
    public Light lampLight;

    protected override void Awake()
    {
        base.Awake();
        lampLight.enabled = false;
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        base.Interact(heldObject, player);
        if (lampLight.enabled)
        {
            lampLight.enabled = false;
            gameDataScriptable.turnedOnLight = false;
        }
        else
        {
            lampLight.enabled = true;
            gameDataScriptable.turnedOnLight = true;
        }
    }

    public override void StartDay()
    {
        base.StartDay();
        gameDataScriptable.turnedOnLight = false;
    }
}
