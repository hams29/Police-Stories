using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGadgetTable", menuName = "Table/Gadget Table")]
public class GadgetTable : ScriptableObject
{
    [Header("Gadget Prefab")]
    public GameObject gadgetPrefab;

    [Header("Gadget Image")]
    public Sprite gadgetImage;
}
