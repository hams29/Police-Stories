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

    private void Update()
    {
        //�}�E�X���E�ɓ������Ƃ�
        if(mousePosition.x > oldMousePosition.x + deadZone)
        {
            float move = mousePosition.x - oldMousePosition.x;
            transform.Rotate(Vector3.up, mouseSensivity * move);
            oldMousePosition = mousePosition;
        }
        //�}�E�X�����ɓ������Ƃ�
        else if (mousePosition.x < oldMousePosition.x - deadZone)
        {
            float move = mousePosition.x - oldMousePosition.x;
            transform.Rotate(Vector3.up, mouseSensivity * move);
            oldMousePosition = mousePosition;
        }
        //�}�E�X�������Ȃ������Ƃ�
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
