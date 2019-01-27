using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : Interactable
{
    public GameDataScriptable gameDataScriptable;
    public ParticleSystem steam;

    // Start is called before the first frame update
    public override void StartDay()
    {
        base.StartDay();
        gameDataScriptable.tookShower = false;
    }

    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        if (gameDataScriptable.gotDressed)
            gameDataScriptable.gotDressed = false;
        gameDataScriptable.tookShower = true;

        StartCoroutine(TakeShower(player.transform.parent));
    }

    private IEnumerator TakeShower(Transform player)
    {
        var emission = steam.emission;
        steam.Play();
        player.GetComponent<PlayerMovement>().enabled = false;
        emission.rateOverTimeMultiplier = 10;

        Vector3 initPosition = player.position;
        player.position = transform.position;
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        Quaternion initRotation = Camera.main.transform.rotation;

        Camera.main.transform.position = new Vector3(5.546f, 0.8f, -2.404f);
        Camera.main.transform.rotation = Quaternion.Euler(22.036f, 47.637f, 2.603f);

        //Play sounds of shower
        yield return new WaitForSeconds(4f);

        Camera.main.transform.rotation = initRotation;
        Camera.main.transform.localPosition = new Vector3(0, 2, -0.25f);

        player.position = initPosition;
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<Rigidbody>().useGravity = true;
        emission.rateOverTimeMultiplier = 0;
        player.GetComponent<PlayerMovement>().enabled = true;
    }

}
