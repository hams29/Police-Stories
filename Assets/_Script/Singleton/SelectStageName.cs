using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageName : MonoBehaviour
{
    [SerializeField] private string[] selectStageName;
    int selectNo = 0;


    public string GetStageName() => selectStageName[selectNo];

}
