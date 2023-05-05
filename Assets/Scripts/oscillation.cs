using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscillation : MonoBehaviour
{
    Vector3 initPos;
    public float amp;
    public float freq;
    public float axis;
    private void Start()
    {
        initPos = transform.position;
    }

    private void Update()
    {
        switch (axis)
        {
            case 0:
                transform.position = new Vector3(Mathf.Sin(Time.time * freq) * amp + initPos.x, initPos.y, initPos.z);
                break;
            case 1:
                transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp + initPos.y, initPos.z);
                break;
            case 2:
                transform.position = new Vector3(initPos.x, initPos.y, Mathf.Sin(Time.time * freq) * amp + initPos.z);
                break;
            default:
                break;
        }
    }
}
