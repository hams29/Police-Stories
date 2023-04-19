using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private mainWeaponData weaponData;
    private float shotTime;

    public int[] currentAmmo { get; private set; } = new int[3];

    private void Start()
    {
        for (int i = 0; i > currentAmmo.Length; i++)
            currentAmmo[i] = weaponData.maxAmmo;
    }

    public void Shot()
    {
        if(Time.time > shotTime + weaponData.shotResponce)
        {
            shotTime = Time.time;
            //TODO::Gun::Œ‚‚Á‚½‚Ìˆ—
            Debug.Log("bang!!");
        }
    }
}
