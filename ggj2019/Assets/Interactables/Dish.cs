using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : Pickup
{

    public bool hasFood = false, clean = true, eating = false;
    public GameDataScriptable gameDataScriptable;
    public GameObject food;
    private MeshRenderer meshRenderer;

    public Texture dirtyTex;

    public override void StartDay()
    {
        base.StartDay();
        gameDataScriptable.hasEaten = false;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 5)
        {
            //Dish.break;
        }
    }

    private void FixedUpdate()
    {
        meshRenderer.material.mainTexture = clean ? null : dirtyTex;
    }

    public override void OnDrop()
    {
        if (hasFood)
        {
            print("has food");
            if (!eating)
            {
                print("eat");
                StartCoroutine(Eat());
            }
            else
            {
                StartCoroutine(regrab());
            }
            eating = true;
        }
        else if (!eating)
        {
            base.OnDrop();
        }
    }

    public void AddFood()
    {
        //Change the model
        food.SetActive(true);
        hasFood = true;
    }

    private IEnumerator regrab()
    {
        yield return new WaitForEndOfFrame();
        Camera.main.GetComponent<PlayerInteractor>().SetHeldObject(this, false);
    }

    private IEnumerator Eat()
    {
        yield return new WaitForEndOfFrame();
        Camera.main.GetComponent<PlayerInteractor>().SetHeldObject(this, false);
        //Play sound of coffee drinking
        yield return new WaitForSeconds(3f);
        food.SetActive(false);
        gameDataScriptable.hasEaten = true;
        hasFood = false;
        clean = false;
        eating = false;
        //Camera.main.GetComponent<PlayerInteractor>().Drop();
    }
}
