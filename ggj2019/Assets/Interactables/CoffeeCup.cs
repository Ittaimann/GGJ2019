using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : Pickup
{
    public bool hasCoffee = false;
    public GameDataScriptable gameDataScriptable;
    private bool drinking = false;

    public override void StartDay()
    {
        base.StartDay();
        gameDataScriptable.drankCoffee = false;
    }

    public override void OnDrop()
    {
        if(hasCoffee)
        {
            if (!drinking)
                StartCoroutine(DrinkCoffee());
            drinking = true;
        }
        else if (!drinking)
        {
            base.OnDrop();
        }
    }

    private IEnumerator DrinkCoffee()
    {
        yield return new WaitForEndOfFrame();
        Camera.main.GetComponent<PlayerInteractor>().SetHeldObject(this, false);
        //Play sound of coffee drinking
        print("drinking");
        yield return new WaitForSeconds(3f);
        print("done");
        gameDataScriptable.drankCoffee = true;
        hasCoffee = false;
        drinking = false;
        //Camera.main.GetComponent<PlayerInteractor>().Drop();
    }
}
