using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGadget : GadgetBase
{
    public DebugGadget() : base() { }

    public override void UseGadget()
    {
        base.UseGadget();
    }
    public override void EndGadget()
    {
        base.EndGadget();
    }

    public override void LogicUpdate()
    {
        Debug.Log("�f�o�b�O�K�W�F�b�g���g���Ă܂��B���Ɍ��ʂ͂Ȃ��I�I");
    }
}
