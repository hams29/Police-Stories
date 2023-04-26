using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    public Gun gun { get; private set; }
    private GameObject mainWeapon;

    [SerializeField]
    private GameObject handGunSet;


    // Start is called before the first frame update
    void Start()
    {
        //mainWeapon = Instantiate(PC.Inventory.mainWeapon, handGunSet.transform);
        gun = mainWeapon.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {        
        //Debug
        Debug.Log(gun.GetShotTime());
    }
}
