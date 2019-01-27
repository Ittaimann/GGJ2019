using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Pickup
{
    private Transform playerTransform;
    public override void OnDrop()
    {
        base.OnDrop();
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 750f);
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        base.Interact(heldObject, player);
        playerTransform = player.transform.parent;
    }
}
