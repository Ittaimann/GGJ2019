using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Interactable
{
    private bool isCooked = false;
    public override void Interact(Pickup heldObject, PlayerInteractor player)
    {
        if(heldObject is RawFood)
        {
            GameObject food = heldObject.gameObject;
            player.Drop();
            Destroy(food);
            StartCoroutine(Cook(heldObject.gameObject));
        }

        if(heldObject is Dish && isCooked)
        {
            heldObject.GetComponent<Dish>().AddFood();
            isCooked = false;
        }

    }

    private IEnumerator Cook(GameObject food)
    {
        Destroy(food.GetComponent<RawFood>());
        yield return new WaitForSeconds(5f);
        isCooked = true;
        
    }
}
