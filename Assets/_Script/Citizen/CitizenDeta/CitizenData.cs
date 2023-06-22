using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newCitizenData",menuName ="Data/Citizen Data/Base Data")]
public class CitizenData : ScriptableObject
{
    [Header("Citizen HP")]
    public float maxHP = 100.0f;

    [Header("Citizen Detantion Time"),Tooltip("s–¯‚ğS‘©‚·‚éŠÔ")]
    public float detectionTime = 2.0f;
}
