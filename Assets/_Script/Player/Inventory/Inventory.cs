using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject mainWeapon { get; private set; }
    public List<GameObject> gadgetObjects { get; private set; } = new List<GameObject>();
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
    private List<GameObject> debugGadgets = new List<GameObject>();

    private void Awake()
    {
        if(!isPlayer)
        {
            if (debugMainWeapon != null)
                mainWeapon = debugMainWeapon;

            if (mainWeapon != null)
                gunType = mainWeapon.GetComponent<Gun>().GetMainWeaponData().gunType;
        }
        /*
        else
        {
            if(debugGadgets.Count > 0)
            {
                foreach (GadgetBase gadgetBase in debugGadgets)
                    gadgets.Add(Instantiate(gadgetBase.gameObject, gadgetHolder).GetComponent<GadgetBase>());
            }
        }
        */
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

    public void SetGadget()
    {
        if(gameManager.GameManager != null)
        {
            if (gameManager.GameManager.gadgetObjects.Count > 0)
            {
                SetGadget(gameManager.GameManager.gadgetObjects);
            }
            else if (debugGadgets.Count > 0)
                SetDebugGadget();
        }
        else if(debugGadgets.Count > 0)
        {
            SetDebugGadget();                
        }
    }

    private void SetDebugGadget()
    {
        foreach (GameObject gadgetObject in debugGadgets)
        {
            gadgetObjects.Add(Instantiate(gadgetObject, gadgetHolder));
        }

        foreach (GameObject gadgetObject in gadgetObjects)
        {
            gadgets.Add(gadgetObject.GetComponent<GadgetBase>());
        }
    }

    private void SetGadget(List<GameObject> gadgetObjects)
    {
        foreach(GameObject gadgetObject in gadgetObjects)
        {
            this.gadgetObjects.Add(Instantiate(gadgetObject, gadgetHolder));
        }
        
        foreach(GameObject gadgetObject in this.gadgetObjects)
        {
            gadgets.Add(gadgetObject.GetComponent<GadgetBase>());
        }
    }

    /*
    public void SetMainWeapon(GameObject mainWeapon)
    {
        this.mainWeapon = mainWeapon;
    }
    */
}
