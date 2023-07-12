using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour
{
    [SerializeField]
    private float angle = 45.0f;

    public bool isSearchFind { get; private set; }
    public bool isSearchDead { get; private set; }
    public Vector3 SearchPos { get; private set; }
    public GameObject findObject { get; private set; }

    private void Awake()
    {
        //isPlayerFind = false;
        //isPlayerDead = false;


        //isSearchFind = false;
        isSearchFind = false;
        isSearchDead = false;
    }

    private void Update()
    {
        if(isSearchFind)
        {
            States state = null;
            findObject.GetComponentInChildren<Core>().GetCoreComponent(ref state);
            if(state != null)
            {
                if(state.dead)
                {
                    isSearchFind = false;
                    findObject = null;
                }
            }

            if(findObject != null)
            {
                if(CheckBetweenObject(findObject))
                {
                    isSearchFind = false;
                    findObject = null;
                }
            }
            else
            {
                isSearchFind = false;
                findObject = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        /*
        if(CheckView(other, "Player"))
        {
            playerPos = other.transform.position;
            States state = null;
            other.GetComponentInChildren<Core>().GetCoreComponent(ref state);
            if (state != null)
                isPlayerDead = state.dead;
            isPlayerFind = true;
        }

        if(CheckView(other, "Friend"))
        {
            friendrPos = other.transform.position;
            States state = null;
            other.GetComponentInChildren<Core>().GetCoreComponent(ref state);
            if (state != null)
                isFriendDead = state.dead;
            isFriendFind = true;
        }
        */

        if (CheckView(other, "Player") || CheckView(other, "Friend"))
        {
            //TODO::���g            
            States state = null;
            other.GetComponentInChildren<Core>().GetCoreComponent(ref state);
            if (state != null)
            {
                if (!state.dead)
                {
                    isSearchFind = true;
                    SearchPos = other.transform.position;
                    findObject = other.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /*
        if (other.gameObject.tag == "Player")
            isPlayerFind = false;

        if (other.gameObject.tag == "Friend")
            isFriendFind = false;
        */

        if(other.gameObject == findObject)
        {
            isSearchFind = false;
        }
    }

    private bool CheckView(Collider other,string tag)
    {
        bool ret = false;

        if (other.gameObject.tag == tag)
        {
            //���E�̊p�x���Ɏ��܂��Ă��邩
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            //target_angle��angle�Ɏ��܂��Ă��邩�ǂ���
            if (target_angle < angle)
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                Debug.DrawRay(pos, posDelta, Color.red, 0.5f);
                //Ray���g�p����target�ɓ������Ă��邩����
                if (Physics.Raycast(pos, posDelta, out RaycastHit hit))
                {
                    if (hit.transform.gameObject.tag == tag)
                    {
                        if (hit.collider == other)
                        {
                            //���E���Ɏ��܂��Ă���ꍇ
                            ret = true;
                        }
                    }
                }
            }
        }

        return ret;
    }


    //False :�@�Ԃɑ��̃I�u�W�F�N�g����   True : ���̃I�u�W�F�N�g����
    private bool CheckBetweenObject(GameObject obj)
    {
        bool ret = false;
        Vector3 posDelta = obj.transform.position - this.transform.position;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        //Ray���g�p����target�ɓ������Ă��邩����
        if (Physics.Raycast(pos, posDelta, out RaycastHit hit))
        {
            if (hit.transform.gameObject.tag == tag)
            {
                if (hit.collider == obj)
                {
                    ret = false;
                }
                else
                {
                    ret = true;
                }
            }
        }

        return ret;
    }
}
