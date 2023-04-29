using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunSearch : MonoBehaviour
{
    [SerializeField]
    private float angle = 45.0f;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            //視界の角度内に収まっているか
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward,posDelta);

            //target_angleがangleに収まっているかどうか
            if (target_angle < angle) 
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                Debug.DrawRay(pos, posDelta, Color.red, 0.5f);
                //Rayを使用してtargetに当たっているか判別
                if (Physics.Raycast(pos,posDelta,out RaycastHit hit)) 
                {
                    if(hit.collider == other)
                    {
                        //視界内に収まっている場合
                        SetShow(other.gameObject);
                    }
                    else if(hit.collider.gameObject.layer == other.gameObject.layer)
                    {
                        SetShow(other.gameObject);
                    }
                    else
                    {
                        //ターゲットとプレイヤーの間に別のオブジェクトが入った場合
                        SetBlind(other.gameObject);
                    }
                }
            }
            else
            {
                //角度内に収まっていない場合
                SetBlind(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            //視界からターゲットが抜けた場合
            SetBlind(other.gameObject);
        }
    }

    private void SetBlind(GameObject other)
    {
        Core otherCore = other.GetComponentInChildren<Core>();
        if (otherCore != null)
        {
            //CoreComponentの取得
            Show otherShow = null;
            otherCore.GetCoreComponent(ref otherShow);
            if (otherShow != null)
            {
                otherShow.BlindObject();
            }
        }
    }

    private void SetShow(GameObject other)
    {
        Core otherCore = other.GetComponentInChildren<Core>();
        if (otherCore != null)
        {
            //CoreComponentの取得
            Show otherShow = null;
            otherCore.GetCoreComponent(ref otherShow);
            if (otherShow != null)
            {
                otherShow.ShowObject();
            }
        }
    }
}
