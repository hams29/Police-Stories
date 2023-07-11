using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFriendData", menuName = "Data/Friend Data/Base Data")]
public class FriendData : ScriptableObject
{
    [Header("Friend HP")]
    public float maxHP = 100.0f;

    [Header("Move State")]
    public float moveSpeed = 5.0f;
    public float runSpeed = 8.0f;

    [Header("Move Player Around"), Tooltip("ƒvƒŒƒCƒ„[‚É‹ß‚Ã‚­‹——£")]
    public float playerAround = 3.0f;

    [Header("Interact")]
    public float interactDistance = 2.5f;
    public string interactLayerName;

}
