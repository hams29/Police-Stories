using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour
{
    [SerializeField]
    private float angle = 45.0f;

    public bool isPlayerFind { get; private set; }
    public Vector3 playerPos { get; private set; }

    private void Awake()
    {
        isPlayerFind = false;
    }

    private void Update()
    {
        Debug.Log(isPlayerFind);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
                    if (hit.collider == other)
                    {
                        //視界内に収まっている場合
                        playerPos = other.transform.position;
                        isPlayerFind = true;
                    }
                    else if (hit.collider.gameObject.layer == other.gameObject.layer)
                    {
                        playerPos = other.transform.position;
                        isPlayerFind = true;
                    }
                    else
                    {
                        //ターゲットとプレイヤーの間に別のオブジェクトが入った場合
                        isPlayerFind = false;
                    }
                }
            }
            else
            {
                //角度内に収まっていない場合
                isPlayerFind = false;
            }
        }
    }
}
