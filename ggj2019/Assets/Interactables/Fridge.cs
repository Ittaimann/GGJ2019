using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interactable
{
    [SerializeField]
    protected RawFood foodPrefab;

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        if (heldObject is RawFood)
            return;
        player.Drop();
        player.SetHeldObject(Instantiate(foodPrefab, player.transform.position, Quaternion.identity), true);
    }
}
