using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Toilet : Interactable
{
    public GameDataScriptable gameDataScriptable;
    AudioSource sfx;
    ParticleSystem vfx;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        sfx = GetComponent<AudioSource>();
        vfx = GetComponentInChildren<ParticleSystem>();
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        base.Interact(heldObject, player);
        sfx.Play();
        vfx.Play();
        //bathroom complete
    }

    public override void StartDay()
    {
        base.StartDay();
        gameDataScriptable.usedToilet = false;
    }
}
