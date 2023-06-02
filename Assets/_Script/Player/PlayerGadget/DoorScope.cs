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
    private Vector3 cameraRot;
    private DoorCameraView cameraView;

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

        cameraPos = doorVariable.doorCameraPos.transform.position;
        cameraRot = doorVariable.GetDoorScopeAngle(player.transform.position).normalized;
        //subCamera.transform.position = transform.InverseTransformPoint(cameraPos);
        subCamera.transform.position = cameraPos;
        subCamera.transform.LookAt(cameraPos + cameraRot);
        player.search.SetThrowObject(doorObj);

        cameraView = subCamera.GetComponent<DoorCameraView>();
        cameraView?.SetInitMousePosition(player.inputController.MousePosition);
        Cursor.visible = false;
        player.search.SetAllShow(true);
    }
    public override void EndGadget()
    {
        base.EndGadget();

        mainCamera.SetActive(true);
        subCamera.SetActive(false);
        player.search.DelThrowObject();
        Cursor.visible = true;
        player.search.SetAllShow(false);
    }

    public override void LogicUpdate()
    {
        if (isEnd)
            return;

        cameraView?.SetMousePosition(player.inputController.MousePosition);
        mainCamera.SetActive(false);
        subCamera.SetActive(true);
    }

    public void SetCameraPos(Vector3 cpos) { cameraPos = cpos; }
}
