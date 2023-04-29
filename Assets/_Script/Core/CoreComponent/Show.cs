using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : CoreComponent,ILogicUpdate
{
    Renderer rend;
    Material material;

    [SerializeField]
    Material transparentMaterial;

    protected override void Awake()
    {
        base.Awake();

    }

    private void Start()
    {
        Renderer[] rend = transform.root.GetComponentsInChildren<Renderer>();
        this.rend = rend[0];
        material = this.rend.material;

        if (transparentMaterial == null)
            Debug.Log("Transparent MaterialÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");

        //èâÇﬂÇÕå©Ç¶Ç»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
        BlindObject();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    #region Set Function
    public void BlindObject()
    {
        /*
        material.shader = Shader.Find("Transparent");
        Color color = material.color;
        color.a = 0.5f;
        material.color = color;
        */

        rend.material = transparentMaterial;
    }

    public void ShowObject()
    {
        /*
        Color color = material.color;
        color.a = 1.0f;
        material.color = color;

        material.shader = normShader;
        */

        rend.material = material;
    }
    #endregion
}
