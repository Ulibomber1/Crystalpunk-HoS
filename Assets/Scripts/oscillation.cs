using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscillation : MonoBehaviour
{
    Vector3 initPos;
    public float amp;
    public float freq;
    public float axis;
    private Rigidbody rb;
    private void Start()
    {
        initPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        switch (axis)
        {
            case 0:
                rb.MovePosition(new Vector3(Mathf.Sin(Time.time * freq) * amp + initPos.x, initPos.y, initPos.z));
                break;
            case 1:
                rb.MovePosition(new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp + initPos.y, initPos.z));
                break;
            case 2:
                rb.MovePosition(new Vector3(initPos.x, initPos.y, Mathf.Sin(Time.time * freq) * amp + initPos.z));
                break;
            default:
                break;
        }
    }
}
