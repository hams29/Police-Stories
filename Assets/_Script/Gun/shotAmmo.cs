using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotAmmo : MonoBehaviour
{
    [SerializeField]
    private LayerMask ammoAble;

    private float damage;

    private void Awake()
    {
        damage = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareLayer(ammoAble,other.gameObject.layer))
        {
            other.gameObject.GetComponentInChildren<Damage>().addDamage(damage);
            Debug.Log("Hit!!");
            Destroy(this.gameObject);
        }
    }

    public void SetDamageValue(float damage) => this.damage = damage;

    private bool CompareLayer(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }
}
