using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Application.Quit();
    }
}
