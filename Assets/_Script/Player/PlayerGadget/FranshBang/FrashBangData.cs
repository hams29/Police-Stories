using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFrashBangData", menuName = "Data/Gadget Data/Frashbang/Base Data")]
public class FrashBangData : ScriptableObject
{
    [Header("Frashbang Explosion Time"), Tooltip("�t���b�V���o���𓊂��Ă��甚������܂ł̎���")]
    public float frashbangExplosionTime = 2.0f;

    [Header("Frashbang Hold Time"), Tooltip("�t���b�V���o���������������̌p������")]
    public float frashbangHoldTime = 4.0f;
}
