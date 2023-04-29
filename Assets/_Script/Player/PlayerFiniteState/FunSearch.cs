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

            if(target_angle < angle) //target_angleがangleに収まっているかどうか
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                Debug.DrawRay(pos, posDelta, Color.red, 0.5f);
                //Rayを使用してtargetに当たっているか判別
                if (Physics.Raycast(pos,posDelta,out RaycastHit hit)) 
                {
                    if(hit.collider == other)
                    {
                        //Coreの取得
                        Core otherCore = other.transform.GetComponentInChildren<Core>();
                        if(otherCore != null)
                        {
                            //CoreComponentの取得
                            Show otherShow = null;
                            otherCore.GetCoreComponent(ref otherShow);
                            if(otherShow != null)
                            {
                                otherShow.ShowObject();
                            }
                        }
                    }
                }
            }
        }
    }
}
