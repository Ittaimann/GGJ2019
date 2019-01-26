using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base class for providing behaviors for interactable objects that can be picked up. Actual interactables should probably be children of this.
/// </summary>
public class Pickup : Interactable
{
    Collider col;
    private void Start()
    {
        col = GetComponent<Collider>();
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        if (heldObject == null)
        {
            player.SetHeldObject(this);
            Debug.Log("pickup");
        }
    }

    public virtual void OnPickup()
    {
        col.enabled = false;
    }

    public virtual void OnDrop()
    {
        col.enabled = true;
    }
}
