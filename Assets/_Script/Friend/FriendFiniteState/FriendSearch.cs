using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendSearch : MonoBehaviour
{
    [SerializeField]
    private float angle = 45.0f;

    public EnemyControllerBase detectedEnemy { get;private set; }
    public bool isDetected { get; private set; }

    private void Awake()
    {
        isDetected = false;
    }

    private void Update()
    {
        if(detectedEnemy != null && isDetected)
        {
            if (detectedEnemy.Core.GetCoreComponent<States>().dead)
            {
                isDetected = false;
                detectedEnemy = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            //視界の角度内に収まっているか
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward, posDelta);

            if (target_angle < angle)
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                Debug.DrawRay(pos, posDelta, Color.red, 0.5f);
                //Rayを使用してtargetに当たっているか判別
                if (Physics.Raycast(pos, posDelta, out RaycastHit hit))
                {
                    if (hit.collider == other && hit.collider.gameObject.tag == "target")
                    {
                        if(!isDetected)
                        {
                            //視界内に収まっている場合
                            EnemyControllerBase enemy = null;
                            if(CheckEnemy(other.gameObject,ref enemy))
                            {
                                detectedEnemy = enemy;
                                //敵の場合の処理
                                if (detectedEnemy.Core.GetCoreComponent<States>().dead)
                                {
                                    detectedEnemy = null;
                                    isDetected = false;
                                }
                                else
                                    isDetected = true;
                            }
                        }
                    }
                    else
                    {
                        //ターゲットとプレイヤーの間に別のオブジェクトが入った場合
                        detectedEnemy = null;
                        isDetected = false;
                    }
                }
            }
            else
            {
                //角度内に収まっていない場合
                detectedEnemy = null;
                isDetected = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == detectedEnemy)
        {
            detectedEnemy = null;
            isDetected = false;
        }
    }

    private bool CheckEnemy(GameObject obj,ref EnemyControllerBase enemy)
    {
        bool ret = false;
        enemy = obj.GetComponent<EnemyControllerBase>();
        if (enemy != null) ret = true;
        else enemy = null;

        return ret;
    }
}
