using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwich : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualPlayerCamera;

    private int priority;

    private void Start()
    {
        priority = 0;
    }

    public void EndAnimation()
    {
        priority = virtualPlayerCamera.Priority;
    }

}
