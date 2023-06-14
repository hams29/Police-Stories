using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGunTabel",menuName = "Table/Gun Tabel")]
public class GunTable : ScriptableObject
{
    [Header("Gun Prefab")]
    public GameObject gunPrefab;

    [Header("Gun Image")]
    public Sprite gunSprite;
}
