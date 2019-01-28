using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print("test");
        Application.Quit();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
