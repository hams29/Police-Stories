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
        Debug.Log("デバッグガジェットを使ってます。特に効果はなし！！");
    }
}
