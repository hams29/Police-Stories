using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
     [SerializeField] Animator doorAC;

    bool isOpened = false;

    // Start is called before the first frame update
    private void Awake()
    {
        doorAC = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isOpened)
        {
            isOpened = true;
            doorAC.SetTrigger("open");
            doorAC.SetBool("opened",isOpened);
        }
        else if (Input.GetMouseButtonDown(0) && isOpened)
        {
            isOpened = false;
            doorAC.SetTrigger("close");
            doorAC.SetBool("opened", isOpened);
        }

    }
}
