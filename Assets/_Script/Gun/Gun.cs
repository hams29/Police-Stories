
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
    private GameObject muzzleFlash;

    [SerializeField]
    private GameObject gunMuzzle;

    [SerializeField]
    private bool isHaveEnemy = false;

    private bool fullAuto;
    private bool lastGunShot;

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
        lastGunShot = false;
    }

    public void Shot()
    {
        //Debug.Log("Time.time : " + Time.time);
        //Debug.Log("shotTime : " + shotTime);
        if (Time.time > shotTime + weaponData.shotWaitTime && currentMagazine[nowMagazine] > 0)
        {
            shotTime = Time.time;
            currentMagazine[nowMagazine]--;
            //Vector3 pos = GameObject.Find("gunMuzzle").transform.position;
            Vector3 pos = gunMuzzle.transform.position;
            GameObject shot = Instantiate(shotAmmoPrefab, pos, Quaternion.Euler(new Vector3(0, 90, 0)));
            Instantiate(muzzleFlash, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
            //shot.GetComponent<Rigidbody>().AddForce(this.transform.right * weaponData.ammoSpeed, ForceMode.Impulse);
            shot.GetComponent<Rigidbody>().AddForce(transform.root.transform.forward * weaponData.ammoSpeed + new Vector3(Random.Range(Mathf.Abs(weaponData.shotReaction) * -1, Mathf.Abs(weaponData.shotReaction)), 0, Random.Range(Mathf.Abs(weaponData.shotReaction) * -1, Mathf.Abs(weaponData.shotReaction))), ForceMode.Impulse);
            shot.GetComponent<shotAmmo>().SetDamageValue(weaponData.shotDamage);
            shot.GetComponent<shotAmmo>().SetShotObject(transform.root.gameObject);

            //カメラを揺らす（プレイヤー射撃時のみ）
            if(!isHaveEnemy)
            {
                var source = GetComponent<Cinemachine.CinemachineImpulseSource>();
                source.GenerateImpulse();
            }

            lastGunShot = true;
        }
        else
            lastGunShot = false;
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

        Debug.Log(transform.root.gameObject.name +  " now magazine is " + nowMagazine);
        Debug.Log(transform.root.gameObject.name + " magazine current ammo is " + currentMagazine[nowMagazine]);
    }

    public mainWeaponData GetMainWeaponData() { return weaponData; }
    public bool GetLastGunShot() { return lastGunShot; }
    //Enemy用
    public void MaxAmmo()
    {
        currentMagazine[nowMagazine] = weaponData.maxAmmo;
    }
}
