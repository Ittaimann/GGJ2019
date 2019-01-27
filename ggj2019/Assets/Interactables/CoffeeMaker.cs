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
        float counter = 0;
        MeshRenderer meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        while(counter < 10)
        {
            meshRenderer.material.SetFloat("_CLipPos", Mathf.Lerp(-.65f, -.46f, counter / 10f));
            yield return new WaitForSeconds(0.1f);
            counter += 0.1f;
        }
        //Play sound of coffee finishing
        print("coffee ready");
        coffeeMade = true;
    }
}
