using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    public Transform cam;

    void LateUpdate()
    {
        transform.LookAt(cam);
    }
}
