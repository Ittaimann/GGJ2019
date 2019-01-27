﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : Interactable
{

    public GameDataScriptable gameDataScriptable;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        doorOpen();
        base.Interact(heldObject, player);
        if (gameDataScriptable.fedCat
            && gameDataScriptable.gotDressed
            && gameDataScriptable.hasEaten
            && gameDataScriptable.tookShower
            && gameDataScriptable.turnedOffAlarm
            && gameDataScriptable.turnedOnLight
            && gameDataScriptable.usedToilet
            && gameDataScriptable.washedDishes)
        {
        }
    }

    void doorOpen() {
        Quaternion start = transform.rotation;
        Quaternion end = start * Quaternion.AngleAxis(90, Vector3.up);
        Callback.DoLerp((l) => transform.rotation = Quaternion.Lerp(start, end, l), 2.0f, this);
    }
}