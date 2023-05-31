using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorVariable : MonoBehaviour
{
    public GameObject doorCameraPos;
    public bool playerRight;

    public void Start()
    {
        playerRight = false;
    }

    public Vector3 GetDoorScopeAngle(Vector3 ppos)
    {
        Vector3 ret = new Vector3(0, 0, 0);
        if(playerRight)
        {
            ret = doorCameraPos.transform.right * -1;
        }
        else
        {
            ret = doorCameraPos.transform.right;
        }
        return ret;
    }

    public void SetPlayerRight(bool flg)
    {
        playerRight = flg;
    }
}
