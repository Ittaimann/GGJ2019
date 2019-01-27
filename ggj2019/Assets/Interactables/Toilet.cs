using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Toilet : Interactable
{
    public GameDataScriptable gameDataScriptable;
    AudioSource sfx;
    ParticleSystem vfx;

    public GameObject lidHinge;
    private bool canUse = true;

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
        if (!canUse)
            return;
        gameDataScriptable.usedToilet = true;
        StartCoroutine(UseToilet(player.transform.parent.gameObject));

        //bathroom complete
    }

    public override void StartDay()
    {
        base.StartDay();
        gameDataScriptable.usedToilet = false;
    }

    private IEnumerator UseToilet(GameObject player)
    {
        canUse = false;
        float angle = 0;

        player.GetComponent<PlayerMovement>().enabled = false;
        while(angle < 90)
        {
            lidHinge.transform.rotation = Quaternion.Euler(angle, 0, 0);
            angle+=3;
            yield return new WaitForSeconds(0.01f);
        }
        sfx.Play();
        vfx.Play();
        yield return new WaitForSeconds(2f);
        while(angle > 0)
        {
            lidHinge.transform.rotation = Quaternion.Euler(angle, 0, 0);
            angle-=3;
            yield return new WaitForSeconds(0.01f);
        }
        lidHinge.transform.rotation = Quaternion.identity;
        player.GetComponent<PlayerMovement>().enabled = true;
        canUse = true;
    }
}
