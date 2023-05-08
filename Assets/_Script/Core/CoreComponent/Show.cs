using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : CoreComponent,ILogicUpdate
{
    Renderer rend;
    Material material;

    [SerializeField]
    Material transparentMaterial;

    public bool isBlind { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        isBlind = false;
    }

    private void Start()
    {
        Renderer[] rend = transform.root.GetComponentsInChildren<Renderer>();
        this.rend = rend[0];
        material = this.rend.material;

        if (transparentMaterial == null)
            Debug.Log("Transparent Material‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB");

        //‰‚ß‚ÍŒ©‚¦‚È‚¢‚æ‚¤‚É‚·‚é
        BlindObject();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    #region Set Function
    public void BlindObject()
    {
        isBlind = true;
        rend.material = transparentMaterial;
    }

    public void ShowObject()
    {
        isBlind = false;
        rend.material = material;
    }
    #endregion
}
