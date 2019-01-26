using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDay : Interactable
{
    public TaskManager taskManager;

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        if(heldObject is Keys)
        {
            taskManager.EndDay();
        }
    }
}
