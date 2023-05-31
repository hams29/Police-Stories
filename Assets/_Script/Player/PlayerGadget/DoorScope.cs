using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScope : GadgetBase
{
    public DoorScope() : base() { }

    [SerializeField]
    private GameObject subCamera;
    private GameObject mainCamera;
    private GameObject doorObj;
    private Vector3 cameraPos;

    private doorVariable doorVariable;
    public override void UseGadget()
    {
        base.UseGadget();
        mainCamera = Camera.main.gameObject;
        subCamera.transform.position = cameraPos;

        bool flg = false;
        if (player.CheckFrontObject("interact", out GameObject hitObj, playerData.meleeDistance))
        {
            doorObj = hitObj;
            doorVariable = hitObj.GetComponent<doorVariable>();
            DoorOpen doorOpen = hitObj.GetComponent<DoorOpen>();
            if (doorVariable != null && !doorOpen.isOpened)
                flg = true;
        }

        if (!flg)
        {
            isEnd = true;
            return;
        }

        cameraPos = doorVariable.doorScopeCenterPos;
        //subCamera.transform.position = transform.InverseTransformPoint(cameraPos);
        subCamera.transform.position = cameraPos;
        player.search.SetThrowObject(doorObj);
    }
    public override void EndGadget()
    {
        base.EndGadget();

        mainCamera.SetActive(true);
        subCamera.SetActive(false);
        player.search.DelThrowObject();
    }

    public override void LogicUpdate()
    {
        if (isEnd)
            return;

        mainCamera.SetActive(false);
        subCamera.SetActive(true);
    }

    public void SetCameraPos(Vector3 cpos) { cameraPos = cpos; }
}
