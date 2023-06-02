using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCameraView : MonoBehaviour
{
    [SerializeField]
    private float cameraMaxAngle = 45.0f;
    [SerializeField]
    private float mouseSensivity = 50.0f;
    [SerializeField]
    private float deadZone = 10.0f;
    private Vector3 oldMousePosition;
    private Vector3 mousePosition;

    Quaternion cameraStartRot;

    private void Update()
    {
        //マウスが右に動いたとき
        if(mousePosition.x > oldMousePosition.x + deadZone)
        {
            //Quaternion targetRotation = Quaternion.Euler(0f, transform.rotation.y + mouseSensivity, 0f);
            transform.Rotate(Vector3.up, mouseSensivity * Time.deltaTime);
        }
        //マウスが左に動いたとき
        else if (mousePosition.x < oldMousePosition.x - deadZone)
        {
            transform.Rotate(Vector3.up, -mouseSensivity * Time.deltaTime);
        }
        //マウスが動かなかったとき
        else
        {

        }
    }
    public void SetInitMousePosition(Vector3 mpos) 
    {
        mousePosition = mpos; 
        oldMousePosition = mpos;
    }
    public void SetMousePosition(Vector3 mpos)
    {
        mousePosition = mpos;
    }

}
