using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : Interactable
{
    private AudioSource sfx;
    private ParticleSystem vfx;
    private Dish dish;
    public GameDataScriptable gameDataScriptable;

    protected override void Awake()
    {
        base.Awake();
        sfx = GetComponent<AudioSource>();
        vfx = GetComponentInChildren<ParticleSystem>();
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
            dish.clean = true;
            sfx.Play();
            vfx.Play();
            gameDataScriptable.washedDishes = true;
        }
    }

    public override void StartDay()
    {
        base.StartDay();
        gameDataScriptable.washedDishes = false;
    }
}
