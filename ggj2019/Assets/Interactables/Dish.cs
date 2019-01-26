using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : Pickup
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 5)
        {
            //Dish.break;
        }
    }
}
