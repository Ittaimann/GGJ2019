using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Interactable
{
    Canvas uiCanvas;
    // Start is called before the first frame update
    void Start()
    {
        uiCanvas = GetComponentInChildren<Canvas>();
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        base.Interact(heldObject, player);
        uiCanvas.enabled = !uiCanvas.enabled;
    }
}
