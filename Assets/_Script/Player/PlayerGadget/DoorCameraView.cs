using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCameraView : MonoBehaviour
{
    [SerializeField]
    private float cameraMaxAngle = 45.0f;
    [SerializeField]
    private float mouseSensivity = 1.0f;
    [SerializeField]
    private float deadZone = 10.0f;
    private Vector3 oldMousePosition;
    private Vector3 mousePosition;
    private float initRotY;

    private void Update()
    {
        Debug.Log(transform.localRotation.eulerAngles);
        //マウスが右に動いたとき
        if(mousePosition.x > oldMousePosition.x + deadZone)
        {            
            float move = mousePosition.x - oldMousePosition.x;
            Quaternion rotation = this.transform.localRotation;
            Vector3 rotationAngles = rotation.eulerAngles;
            var normalizedAngle180 = Mathf.Repeat(rotationAngles.y + 180, 360) - 180;

            if (normalizedAngle180 + (move * mouseSensivity) < initRotY + cameraMaxAngle)
                rotationAngles.y += move * mouseSensivity;
            else
                rotationAngles.y = initRotY + cameraMaxAngle;

            rotation = Quaternion.Euler(rotationAngles);
            this.transform.localRotation = rotation;
            oldMousePosition = mousePosition;
        }
        //マウスが左に動いたとき
        else if (mousePosition.x < oldMousePosition.x - deadZone)
        {
            float move = mousePosition.x - oldMousePosition.x;
            Quaternion rotation = this.transform.localRotation;
            Vector3 rotationAngles = rotation.eulerAngles;
            var normalizedAngle180 = Mathf.Repeat(rotationAngles.y + 180, 360) - 180;

            if (normalizedAngle180 + (move * mouseSensivity) > initRotY - cameraMaxAngle)
                rotationAngles.y += move * mouseSensivity;
            else
                rotationAngles.y = initRotY - cameraMaxAngle;

            rotation = Quaternion.Euler(rotationAngles);
            this.transform.localRotation = rotation;
            oldMousePosition = mousePosition;
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


    private float Cliping(float max,float min,float current)
    {
        float ret = current;

        if (ret > max)
            ret = max;
        else if (ret < min)
            ret = min;


        return ret;
    }

    public void SetRotY(float y) { initRotY = Mathf.Repeat(y + 180, 360) - 180; }
}
