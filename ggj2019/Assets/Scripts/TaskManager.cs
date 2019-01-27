using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{

    //public ScriptableObject gameData;
    private Interactable[] objects;

    private void Start()
    {
        objects = FindObjectsOfType<Interactable>();
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

        //if(allChecksOut)
        //{
        //    GoToEndScene();
        //}
        //else
        {
            //Reload the scene and call the start Day again
            SceneManager.LoadScene(0);
        }
    }
}
