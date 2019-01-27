using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{

    //public ScriptableObject gameData;
    public Interactable[] objects;
    public GameDataScriptable gameDataScriptable;

    private void Start()
    {
        StartDay();
    }

    public void StartDay()
    {
        print("Day Started");
        foreach (Interactable o in objects)
        {
            o.StartDay();
        }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.E))
        //{
        //    EndDay();
        //}
    }


    public void EndDay()
    {
        //Check all data needed inside of gameData scriptable object
        /*
        if (gameDataScriptable.fedCat
            && gameDataScriptable.gotDressed
            && gameDataScriptable.hasEaten
            && gameDataScriptable.tookShower
            && gameDataScriptable.turnedOffAlarm
            && gameDataScriptable.turnedOnLight
            && gameDataScriptable.usedToilet
            && gameDataScriptable.washedDishes)
            */
            if(true)
        {
            SceneManager.LoadScene(1);

        }
        else
        {
            //Reload the scene and call the start Day again
            SceneManager.LoadScene(0);
        }
    }
}
