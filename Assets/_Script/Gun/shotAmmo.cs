using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotAmmo : MonoBehaviour
{
    [SerializeField]
    private LayerMask ammoAble;

    private float ammoDamage;

    private void Awake()
    {
        ammoDamage = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareLayer(ammoAble,other.gameObject.layer))
        {
            Core core = other.GetComponentInChildren<Core>();
            if (core == null)
                return;

            States states = null;
            Damage damage = null;
            core.GetCoreComponent(ref states);
            core.GetCoreComponent(ref damage);

            if (states == null || damage == null)
                return;

            if(!states.dead)
            {
                damage.addDamage(ammoDamage);
                Debug.Log("Hit!!");
                Destroy(this.gameObject);
            }
        }
    }

    public void SetDamageValue(float damage) => this.ammoDamage = damage;

    private bool CompareLayer(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }
}
