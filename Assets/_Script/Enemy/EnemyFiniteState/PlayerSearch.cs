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
            //TODO::中身            
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
            //視界の角度内に収まっているか
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            //target_angleがangleに収まっているかどうか
            if (target_angle < angle)
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                Debug.DrawRay(pos, posDelta, Color.red, 0.5f);
                //Rayを使用してtargetに当たっているか判別
                if (Physics.Raycast(pos, posDelta, out RaycastHit hit))
                {
                    if (hit.transform.gameObject.tag == tag)
                    {
                        if (hit.collider == other)
                        {
                            //視界内に収まっている場合
                            ret = true;
                        }
                    }
                }
            }
        }

        return ret;
    }


    //False :　間に他のオブジェクト無し   True : 他のオブジェクトあり
    private bool CheckBetweenObject(GameObject obj)
    {
        bool ret = false;
        Vector3 posDelta = obj.transform.position - this.transform.position;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        //Rayを使用してtargetに当たっているか判別
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
