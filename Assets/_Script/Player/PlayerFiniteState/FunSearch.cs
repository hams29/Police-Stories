using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunSearch : MonoBehaviour
{
    [SerializeField]
    private float angle = 45.0f;

    public List<EnemyControllerBase> enemyShowList { get; private set; }
    public List<EnemyControllerBase> enemyList { get; private set; }
    public List<CitizenController> citizenShowList { get; private set; }
    private GameObject throwObject;
    private bool isAllShow;

    private void Awake()
    {
        enemyShowList = new List<EnemyControllerBase>();
        enemyList = new List<EnemyControllerBase>();
        citizenShowList = new List<CitizenController>();
        isAllShow = false;
    }

    private void OnTriggerStay(Collider other)
    {
        EnemyControllerBase enemy = other.gameObject.GetComponent<EnemyControllerBase>();
        CitizenController citizen = other.gameObject.GetComponent<CitizenController>();
        if (enemy != null) addEnemyList(enemy);

        if (other.gameObject.tag == "target")
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
                    if(hit.collider == other && hit.collider.gameObject.tag == "target")
                    {
                        //視界内に収まっている場合
                        SetShow(other.gameObject);
                        //EnemyControllerBase enemy = other.gameObject.GetComponent<EnemyControllerBase>();
                        if(enemy != null)
                            addEnemyShowList(enemy);
                        else
                        {
                            //CitizenController citizen = other.gameObject.GetComponent<CitizenController>();
                            if (citizen != null)
                                addCitizenList(citizen);
                        }

                    }
                    else if (throwObject != null)
                    {
                        if (hit.collider.gameObject == throwObject)
                        {
                            SetShow(other.gameObject);
                           // EnemyControllerBase enemy = other.gameObject.GetComponent<EnemyControllerBase>();
                            if (enemy != null)
                                addEnemyShowList(enemy);
                            else
                            {
                                //CitizenController citizen = other.gameObject.GetComponent<CitizenController>();
                                if (citizen != null)
                                    addCitizenList(citizen);
                            }
                        }
                    }
                    else
                    {
                        //ターゲットとプレイヤーの間に別のオブジェクトが入った場合
                        SetBlind(other.gameObject);
                       // EnemyControllerBase enemy = other.gameObject.GetComponent<EnemyControllerBase>();
                        if (enemy != null)
                            delEnemyShowList(enemy);
                        else
                        {
                            //CitizenController citizen = other.gameObject.GetComponent<CitizenController>();
                            if (citizen != null)
                                delCitizenList(citizen);
                        }
                    }
                }
            }
            //すべて表示
            else if(isAllShow)
            {
                SetShow(other.gameObject);
            }
            else
            {
                //角度内に収まっていない場合
                SetBlind(other.gameObject);
                //EnemyControllerBase enemy = other.gameObject.GetComponent<EnemyControllerBase>();
                if (enemy != null)
                    delEnemyShowList(enemy);
                else
                {
                    //CitizenController citizen = other.gameObject.GetComponent<CitizenController>();
                    if (citizen != null)
                        delCitizenList(citizen);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "target")
        {
            //視界からターゲットが抜けた場合
            SetBlind(other.gameObject);
            EnemyControllerBase enemy = other.gameObject.GetComponent<EnemyControllerBase>();
            if (enemy != null)
            {
                delEnemyShowList(enemy);
                delEnemyList(enemy);
            }
            else
            {
                CitizenController citizen = other.gameObject.GetComponent<CitizenController>();
                if (citizen != null)
                    delCitizenList(citizen);
            }
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

    private void addEnemyShowList(EnemyControllerBase enemy)
    {
        if(!enemyShowList.Contains(enemy))
        {
            enemy.SetPlayerOutOfView(false);
            enemyShowList.Add(enemy);
        }
    }

    private void delEnemyShowList(EnemyControllerBase enemy)
    {
        if(enemyShowList.Contains(enemy))
        {
            enemy.SetPlayerOutOfView(true);
            enemy.PlayerOutOfViewTIme();
            enemyShowList.Remove(enemy);
        }
    }

    private void addEnemyList(EnemyControllerBase enemy)
    {
        if (!enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }
    }

    private void delEnemyList(EnemyControllerBase enemy)
    {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }
    }

    private void addCitizenList(CitizenController citizen)
    {
        if (!citizenShowList.Contains(citizen))
        {
            citizenShowList.Add(citizen);
        }
    }

    private void delCitizenList(CitizenController citizen)
    {
        if (citizenShowList.Contains(citizen))
        {
            citizenShowList.Remove(citizen);
        }
    }

    public void SetThrowObject(GameObject obj) { throwObject = obj; }
    public void DelThrowObject() { throwObject = null; }
    public void SetAllShow(bool isShow) { isAllShow = isShow; }
}
