using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FranshBangGadget : GadgetBase
{
    public FranshBangGadget() : base() { }

    [SerializeField]
    private GameObject frashBangPrefab;

    private int nowHaveCount;

    public void Start()
    {
        //nowHaveCountを最大値に設定
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
