using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakeUp : MonoBehaviour
{

    public GameObject topPanel, bottomPanel, text;
    private float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            counter+= 2;
        }
        else
        {
            if(counter > 0)
            {
                counter -= 3;
                if (counter < 0)
                    counter = 0;
            }
        }


        float modifier = Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * counter));
        if(counter <= 180)
        {
            topPanel.transform.localPosition = new Vector3(0, 113 + (68.5f * modifier), 0);
            bottomPanel.transform.localPosition = new Vector3(0, -113 - (68.5f * modifier), 0);
        }
        else if (counter <= 360)
        {
            topPanel.transform.localPosition = new Vector3(0, 113 + (137 * modifier), 0);
            bottomPanel.transform.localPosition = new Vector3(0, -113 - (137 * modifier), 0);
            text.SetActive(true);
        }
        else if (counter <= 430)
        {
            topPanel.transform.localPosition = new Vector3(0, 113 + (250 * modifier), 0);
            bottomPanel.transform.localPosition = new Vector3(0, -113 - (250 * modifier), 0);
            text.SetActive(false);
        }
        else
        {
            topPanel.SetActive(false);
            bottomPanel.SetActive(false);
            counter = 0;
            this.enabled = false;
        }

    }
}
