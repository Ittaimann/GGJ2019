using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMaker : Interactable
{

    public bool coffeeMade = false, makingCoffee = false;
    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        if(heldObject is CoffeeCup && coffeeMade)
        {
            heldObject.GetComponent<CoffeeCup>().hasCoffee = true;
            print("picked up coffee");
        }
        else if(!coffeeMade && !makingCoffee)
        {
            StartCoroutine(MakeCoffee());
        }
    }

    private IEnumerator MakeCoffee()
    {
        makingCoffee = true;
        //Play sound of coffee making
        print("making coffee");
        yield return new WaitForSeconds(10f);
        //Play sound of coffee finishing
        print("coffee ready");
        coffeeMade = true;
    }
}
