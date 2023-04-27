
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private mainWeaponData weaponData;

    [SerializeField]
    private GameObject shotAmmoPrefab;

    private bool fullAuto;

    private float shotTime;

    public int[] currentMagazine { get; private set; } = new int[3];
    private int nowMagazine;

    private void Start()
    {
        for (int i = 0; i < currentMagazine.Length; i++)
            currentMagazine[i] = weaponData.maxAmmo;

        fullAuto = weaponData.fullAuto;
        nowMagazine = 0;
        shotTime = 0;
    }

    public void Shot()
    {
        //Debug.Log("Time.time : " + Time.time);
        //Debug.Log("shotTime : " + shotTime);
        if (Time.time > shotTime + weaponData.shotWaitTime && currentMagazine[nowMagazine] > 0)
        {
            shotTime = Time.time;
            //TODO::Gun::Œ‚‚Á‚½‚Ìˆ—
            currentMagazine[nowMagazine]--;
            Vector3 pos = GameObject.Find("gunMuzzle").transform.position;
            //TODO::Gun::’e‚ğ¶¬‚·‚é‚Æ‚«‚ÌŒü‚«‚Ìˆ—
            GameObject shot = Instantiate(shotAmmoPrefab, pos, Quaternion.Euler(new Vector3(0,90,0)));
            shot.GetComponent<Rigidbody>().AddForce(this.transform.right * weaponData.ammoSpeed, ForceMode.Impulse);
            Debug.Log("bang!!");
        }
    }

    public bool GetFullAuto() { return fullAuto; }

    public float GetShotTime() { return shotTime; }
    public int GetCurrentMagazineAmmo() { return currentMagazine[nowMagazine]; }

    public void Reload()
    {
        if (nowMagazine + 1 >= 3)
            nowMagazine = 0;
        else
            nowMagazine++;

        Debug.Log("now magazine is " + nowMagazine);
        Debug.Log("magazine current ammo is " + currentMagazine[nowMagazine]);
    }
}
