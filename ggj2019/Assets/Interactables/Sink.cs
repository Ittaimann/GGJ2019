using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : Interactable
{
    private AudioSource sfx;
    private Dish dish;

    protected override void Awake()
    {
        base.Awake();
        sfx = GetComponent<AudioSource>();
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        base.Interact(heldObject, player);
        if(heldObject is Dish)
        {
            player.Drop();
            heldObject.transform.position = this.transform.position;
            dish = heldObject as Dish;
        } else if(dish != null && (dish.transform.position - this.transform.position).magnitude < 1)
        {
            //dish.clean = true;
            sfx.Play();
        }
    }
}
