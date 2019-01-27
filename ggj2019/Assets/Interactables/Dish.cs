using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : Pickup
{

    public bool hasFood = false, clean = true, eating = false;
    public GameDataScriptable gameDataScriptable;

    public override void StartDay()
    {
        base.StartDay();
        gameDataScriptable.hasEaten = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 5)
        {
            //Dish.break;
        }
    }

    public override void OnDrop()
    {
        if (hasFood)
        {
            if (!eating)
                StartCoroutine(Eat());
            eating = true;
        }
        else
        {
            base.OnDrop();
        }
    }

    public void AddFood()
    {
        //Change the model
        print("picked up food");
        hasFood = true;
    }

    private IEnumerator Eat()
    {
        yield return new WaitForEndOfFrame();
        Camera.main.GetComponent<PlayerInteractor>().SetHeldObject(this, false);
        //Play sound of coffee drinking
        print("eating");
        yield return new WaitForSeconds(3f);
        print("done");
        gameDataScriptable.hasEaten = true;
        hasFood = false;
        clean = false;
        //Camera.main.GetComponent<PlayerInteractor>().Drop();
    }
}
