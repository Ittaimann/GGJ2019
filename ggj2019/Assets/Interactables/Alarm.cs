using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : Interactable
{
    public GameDataScriptable gameDataScriptable;
    private AudioSource sfx;

    protected override void Awake()
    {
        base.Awake();
        sfx = GetComponent<AudioSource>();
        float alarmVolume = sfx.volume;
        Callback.DoLerp((l) => sfx.volume = alarmVolume * l, 5f, this);
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        base.Interact(heldObject, player);
        if(sfx.isPlaying)
        {
            sfx.Stop();
            gameDataScriptable.turnedOffAlarm = true;
        }
        else
        {
            float alarmVolume = sfx.volume;
            Callback.DoLerp((l) => sfx.volume = alarmVolume * l, 5f, this);
            sfx.Play();
            gameDataScriptable.turnedOffAlarm = false;

        }
    }

    public override void StartDay()
    {
        base.StartDay();
        gameDataScriptable.turnedOffAlarm = false;
    }
}
