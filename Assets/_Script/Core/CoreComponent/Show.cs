using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : CoreComponent,ILogicUpdate
{
    Renderer rend;
    Material material;

    [SerializeField]
    Material transparentMaterial;

    List<Material> materials = new List<Material>();
    List<Renderer> renderers = new List<Renderer>();

    public bool isBlind { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        isBlind = false;
    }

    private void Start()
    {
        /*
        Renderer[] rend = transform.root.GetComponentsInChildren<Renderer>();

        for(int i = 0;i<rend.Length;i++)
        {
            if (rend[i].gameObject.tag == "TargetMesh")
            {
                this.rend = rend[i];
                break;
            }
        }
        material = this.rend.material;

        if (transparentMaterial == null)
            Debug.Log("Transparent MaterialÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");

        //èâÇﬂÇÕå©Ç¶Ç»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
        BlindObject();
        */
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    #region Set Function
    public void BlindObject()
    {
        if (materials.Count <= 0)
            return;

        isBlind = true;
        //rend.material = transparentMaterial;
        foreach(Renderer renderer in renderers)
        {
            renderer.material = transparentMaterial;
        }
    }

    public void ShowObject()
    {
        if (materials.Count <= 0)
            return;

        isBlind = false;
        int count = 0;
        //rend.material = material;
        foreach(Renderer renderer in renderers)
        {
            renderer.material = materials[count];
            count++;
        }
    }

    public void InitMaterials(List<Renderer> mat)
    {
        renderers = mat;
        foreach(Renderer renderer in renderers)
        {
            materials.Add(renderer.material);
        }

        //èâÇﬂÇÕå©Ç¶Ç»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
        BlindObject();
    }
    #endregion
}
