using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSearchPlayerRight : MonoBehaviour
{
    [SerializeField]
    private doorVariable doorVar;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            doorVar.SetPlayerRight(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
            doorVar.SetPlayerRight(false);
    }
}
