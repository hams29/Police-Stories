using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FranshBangGadget : GadgetBase
{
    public FranshBangGadget() : base() { }

    [SerializeField]
    private GameObject frashBangPrefab;

    [SerializeField]
    private int maxFlafhbangCount = 0;

    [SerializeField]
    private float debugThrowPower = 5.0f;
    [SerializeField]
    private Vector3 debugTorque = Vector3.zero;

    private int nowHaveCount;

    public void Start()
    {
        //nowHaveCountを最大値に設定
        nowHaveCount = maxFlafhbangCount;
    }

    public void Update()
    {
    }

    public override void UseGadget()
    {
        base.UseGadget();

        if(nowHaveCount <= 0)
        {
            isEnd = true;
            return;
        }

        //ガジェットを前方に投げる処理
        nowHaveCount--;
        GameObject flashBang = Instantiate(frashBangPrefab, this.gameObject.transform.position, Quaternion.identity);
        Rigidbody fbRB = flashBang.GetComponent<Rigidbody>();
        Vector3 throwVec = this.transform.forward * debugThrowPower;
        fbRB.velocity = throwVec;
        fbRB.AddTorque(debugTorque);
        isEnd = true;
    }

    public override void EndGadget()
    {
        base.EndGadget();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
