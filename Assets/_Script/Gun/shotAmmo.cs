using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotAmmo : MonoBehaviour
{
    [SerializeField]
    private LayerMask ammoAble;

    private float ammoDamage;

    private GameObject shotObject;

    private void Awake()
    {
        ammoDamage = 0;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == shotObject || other.tag == shotObject.tag)
            return;

        if (CompareLayer(ammoAble, other.gameObject.layer))
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

            if (!states.dead)
            {
                damage.addDamage(ammoDamage);
                Debug.Log("Hit!!");
                Destroy(this.gameObject);
            }
        }
        else
            Destroy(this.gameObject);
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == shotObject || collision.gameObject.tag == shotObject.tag)
            return;

        if (CompareLayer(ammoAble, collision.gameObject.layer))
        {
            Core core = collision.gameObject.GetComponentInChildren<Core>();
            if (core == null)
                return;

            States states = null;
            Damage damage = null;
            core.GetCoreComponent(ref states);
            core.GetCoreComponent(ref damage);

            if (states == null || damage == null)
                return;

            if (!states.dead)
            {
                damage.addDamage(ammoDamage);
                Debug.Log("Hit!!");
                Destroy(this.gameObject);
            }
        }
        else
            Destroy(this.gameObject);
    }

    public void SetDamageValue(float damage) => this.ammoDamage = damage;
    public void SetShotObject(GameObject shotObject) => this.shotObject = shotObject;

    private bool CompareLayer(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }
}
