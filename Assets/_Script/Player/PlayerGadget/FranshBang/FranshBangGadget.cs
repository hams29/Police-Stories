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
        //nowHaveCount���ő�l�ɐݒ�
    }

    public override void UseGadget()
    {
        base.UseGadget();

        if(nowHaveCount <= 0)
        {
            isEnd = true;
            return;
        }

        //�K�W�F�b�g��O���ɓ����鏈��
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
