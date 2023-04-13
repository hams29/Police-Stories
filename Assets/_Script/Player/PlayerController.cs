using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody myRB;
    CapsuleCollider myColl;
    PlayerInputHandler inputController;

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myColl = GetComponent<CapsuleCollider>();
        inputController = GetComponent<PlayerInputHandler>();
    }

    private void FixedUpdate()
    {
        
    }
}
