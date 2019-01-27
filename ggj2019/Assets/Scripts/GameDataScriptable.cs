using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameDataScriptable", order = 1)]
public class GameDataScriptable : ScriptableObject
{

    //Shower
    public bool tookShower = false;

    //Dresser
    public bool gotDressed = false;

    //Toilet
    public bool usedToilet = false;

    //KitchenSink
    public bool washedDishes = false;
    
    //Alarm
    public bool turnedOffAlarm = false;

    //Lamp
    public bool turnedOnLight = false;

    //Dish
    public bool hasEaten = false;

    //Cat
    public int catAngerLevel = 0;
    public bool fedCat = false;

    //Vase
    public bool isBroken = false;
}
