using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newCitizenData",menuName ="Data/Citizen Data/Base Data")]
public class CitizenData : ScriptableObject
{
    [Header("Citizen Detantion Time"),Tooltip("�s�����S�����鎞��")]
    public float detectionTime = 2.0f;
}
