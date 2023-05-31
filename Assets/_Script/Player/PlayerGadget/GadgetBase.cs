using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetBase :MonoBehaviour
{
    protected PlayerController player;
    protected PlayerData playerData;
    public GadgetBase() 
    {
    }

    public bool isEnd { get; protected set; }
    public virtual void UseGadget() 
    {
        isEnd = false;
    }

    public virtual void EndGadget()
    {
        isEnd = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public void SetPlayerController(PlayerController player,PlayerData playerData) 
    { 
        this.player = player;
        this.playerData = playerData;
    }
}
