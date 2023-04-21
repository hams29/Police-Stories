using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesroyMe : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;

    private float startTime = 0;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if(Time.time > startTime + destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}
