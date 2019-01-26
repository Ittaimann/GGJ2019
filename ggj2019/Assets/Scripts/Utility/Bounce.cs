using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private float counter = 0;
    public float height;
    public float speed;
    private float initialHeight;

    private void Start()
    {
        initialHeight = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        counter += speed;
        if (counter > 360)
            counter = 0;

        transform.localPosition = new Vector3(transform.localPosition.x,
                                              initialHeight + (height * Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * counter))),
                                              transform.localPosition.z);
    }
}
