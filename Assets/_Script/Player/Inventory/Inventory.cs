using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GunTable mainWeaponTable { get; private set; }

    public mainWeaponData.GunType gunType { get; private set; }
    public GameObject mainWeapon { get; private set; }
    public List<GameObject> gadgetObjects { get; private set; } = new List<GameObject>();
    public List<GadgetTable> gadgetTables { get; private set; } = new List<GadgetTable>();
    public List<GadgetBase> gadgets { get; private set; } = new List<GadgetBase>();
    [SerializeField]
    private bool isPlayer;
    [SerializeField]
    private Transform gadgetHolder;

    //TODO::Inventory::å„Ç≈è¡Ç∑
    [SerializeField]
    private GunTable debugMainWeapon;
    [SerializeField]
    private List<GadgetTable> debugGadgets = new List<GadgetTable>();

    private void Awake()
    {
        if(!isPlayer)
        {
            if (debugMainWeapon != null)
            {
                mainWeaponTable = debugMainWeapon;
                mainWeapon = mainWeaponTable.gunPrefab;
            }

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
            if (gameManager.GameManager.setGun != null)
            {
                mainWeaponTable = gameManager.GameManager.setGun.gunTabele;
                mainWeapon = mainWeaponTable.gunPrefab;
            }
            else if (debugMainWeapon != null)
            {
                mainWeaponTable = debugMainWeapon;
                mainWeapon = mainWeaponTable.gunPrefab;
            }
        }
        else if (debugMainWeapon != null)
        {
            mainWeaponTable = debugMainWeapon;
            mainWeapon = mainWeaponTable.gunPrefab;
        }

        if(mainWeapon != null)
            gunType = mainWeapon.GetComponent<Gun>().GetMainWeaponData().gunType;
    }

    public void SetGadget()
    {
        if(gameManager.GameManager != null)
        {
            gadgetTables = gameManager.GameManager.gadgetObjects;
            if (gadgetTables.Count > 0)
            {
                SetGadget(gadgetTables);
            }
            else if (debugGadgets.Count > 0)
                SetDebugGadget(debugGadgets);
        }
        else if(debugGadgets.Count > 0)
        {               
            SetDebugGadget(debugGadgets);
        }
    }

    private void SetDebugGadget(List<GadgetTable> debugGadgetObjects)
    {
        gadgetTables = debugGadgetObjects;
        SetGadget(gadgetTables);
    }

    private void SetGadget(List<GadgetTable> gadgetObjects)
    {
        foreach(GadgetTable gadgetObject in gadgetObjects)
        {
            this.gadgetObjects.Add(Instantiate(gadgetObject.gadgetPrefab, gadgetHolder));
        }
        
        foreach(GameObject gadgetObject in this.gadgetObjects)
        {
            gadgets.Add(gadgetObject.GetComponent<GadgetBase>());
        }
    }

    //public void SetWeaponActive(bool flg) { mainWeapon.SetActive(flg); }
    //public bool GetWeaponActive() { return mainWeapon.activeSelf; }
}
