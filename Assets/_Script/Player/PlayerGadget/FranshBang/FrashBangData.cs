using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFrashBangData", menuName = "Data/Gadget Data/Frashbang/Base Data")]
public class FrashBangData : ScriptableObject
{
    [Header("Frashbang Explosion Time"), Tooltip("フラッシュバンを投げてから爆発するまでの時間")]
    public float frashbangExplosionTime = 2.0f;

    [Header("Frashbang Hold Time"), Tooltip("フラッシュバンが当たった時の継続時間")]
    public float frashbangHoldTime = 4.0f;
}
