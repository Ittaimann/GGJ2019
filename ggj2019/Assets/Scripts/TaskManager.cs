using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{

    //public ScriptableObject gameData;
    private Interactable[] objects;
    public GameDataScriptable gameDataScriptable;
    private AudioSource alarm;
    private Coroutine day;

    public float dayTimer = 60f;

    private void Start()
    {
        objects = FindObjectsOfType<Interactable>();
        alarm = GetComponent<AudioSource>();
        StartDay();
        day = StartCoroutine(DayTimer());
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

    private IEnumerator DayTimer()
    {
        yield return new WaitForSeconds(60f);
        float counter = 0f;
        alarm.Play();
        while(counter < 3)
        {
            alarm.volume = Mathf.Lerp(0, 1, counter / 3f);
            yield return new WaitForSeconds(0.02f);
            counter += 0.02f;
        }
        gameDataScriptable.loudAlarm = true;
        SceneManager.LoadScene(0);
    }


    //public void EndDay()
    //{
    //    StopCoroutine(day);
    //    //Check all data needed inside of gameData scriptable object
    //    if (gameDataScriptable.fedCat
    //        && gameDataScriptable.gotDressed
    //        && gameDataScriptable.hasEaten
    //        && gameDataScriptable.tookShower
    //        && gameDataScriptable.turnedOffAlarm
    //        && gameDataScriptable.turnedOnLight
    //        && gameDataScriptable.usedToilet
    //        && gameDataScriptable.washedDishes)
    //    {
    //        //SceneManager.LoadScene(1);
    //        gameDataScriptable.loudAlarm = false;

    //    }
    //}
}
