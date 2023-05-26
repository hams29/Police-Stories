using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject mainWeapon { get; private set; }
    public List<GadgetBase> gadgets { get; private set; } = new List<GadgetBase>();
    public mainWeaponData.GunType gunType { get; private set; }
    [SerializeField]
    private bool isPlayer;
    [SerializeField]
    private Transform gadgetHolder;

    //TODO::Inventory::å„Ç≈è¡Ç∑
    [SerializeField]
    private GameObject debugMainWeapon;
    [SerializeField]
    private List<GadgetBase> debugGadgets = new List<GadgetBase>();

    private void Awake()
    {
        if(!isPlayer)
        {
            if (debugMainWeapon != null)
                mainWeapon = debugMainWeapon;

            if (mainWeapon != null)
                gunType = mainWeapon.GetComponent<Gun>().GetMainWeaponData().gunType;
        }
        else
        {
            if(debugGadgets.Count >= 0)
            {
                foreach (GadgetBase gadgetBase in debugGadgets)
                    gadgets.Add(Instantiate(gadgetBase.gameObject, gadgetHolder).GetComponent<GadgetBase>());
            }
        }
    }
    private void Start()
    {
    }

    public void SetMainWeapon()
    {
        if(gameManager.GameManager != null)
        {
            if(gameManager.GameManager.setGun != null)
                mainWeapon = gameManager.GameManager.setGun.gun;
            else if (debugMainWeapon != null)
                mainWeapon = debugMainWeapon;
        }
        else if (debugMainWeapon != null)
            mainWeapon = debugMainWeapon;

        if(mainWeapon != null)
            gunType = mainWeapon.GetComponent<Gun>().GetMainWeaponData().gunType;
    }

    /*
    public void SetMainWeapon(GameObject mainWeapon)
    {
        this.mainWeapon = mainWeapon;
    }
    */
}
