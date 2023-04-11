using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public int axis = 0;
    public float speed = 90;
    // Update is called once per frame
    void Update()
    {
        switch (axis)
        {
            case 0:
                transform.Rotate(new Vector3(speed, 0, 0) * Time.deltaTime * 2);
                break;
            case 1:
                transform.Rotate(new Vector3(0, speed, 0) * Time.deltaTime * 2);
                break;
            case 2:
                transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime * 2);
                break;
        }
    }
}
