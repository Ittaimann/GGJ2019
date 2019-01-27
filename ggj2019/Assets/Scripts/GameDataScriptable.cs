using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameDataScriptable", order = 1)]
public class GameDataScriptable : ScriptableObject
{
    public bool tookShower = false;
    public bool gotDressed = false;
    public bool usedToilet = false;
    public bool washedDishes = false;
    public bool turnedOffAlarm = false;
}
