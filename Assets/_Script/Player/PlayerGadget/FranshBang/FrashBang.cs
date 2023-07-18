using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrashBang : MonoBehaviour
{

    [SerializeField]
    FrashBangData frashBangData;

    private float startTime;
    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        
    }
}
