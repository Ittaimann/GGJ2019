using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : Pickup
{

    public bool hasFood = false, clean = true;
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

    public void AddFood()
    {
        //Change the model
        print("picked up food");
        hasFood = true;
    }

    public void Eat()
    {
        hasFood = false;
        clean = false;
        gameDataScriptable.hasEaten = true;
    }
}
