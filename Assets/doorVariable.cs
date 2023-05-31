using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorVariable : MonoBehaviour
{
    public Vector3 doorScopeCenterPos;

    private void Start()
    {
        //doorScopeCenterPos = transform.TransformPoint(transform.position);
        doorScopeCenterPos += transform.position;
    }
}
