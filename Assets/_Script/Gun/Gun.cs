
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private mainWeaponData weaponData;

    [SerializeField]
    private GameObject shotAmmoPrefab;

    [SerializeField]
    private bool fullAuto;

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
            //TODO::Gun::åÇÇ¡ÇΩéûÇÃèàóù
            Vector3 pos = GameObject.Find("gunMuzzle").transform.position;
            GameObject shot = Instantiate(shotAmmoPrefab, pos, Quaternion.identity);

            Debug.Log("bang!!");
        }
    }

    public bool GetFullAuto() { return fullAuto; }
}
