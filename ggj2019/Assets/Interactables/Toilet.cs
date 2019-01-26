using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Toilet : Interactable
{
    AudioSource sfx;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        sfx = GetComponent<AudioSource>();
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        base.Interact(heldObject, player);
        sfx.Play();
        //bathroom complete
    }
}
