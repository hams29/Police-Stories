using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    private Gun gun;
    private PlayerController PC;
    private GameObject mainWeapon;

    [SerializeField]
    private GameObject debugGun;


    // Start is called before the first frame update
    void Start()
    {
        PC = GetComponent<PlayerController>();


        //TODO::PlayerShotController::‘±‚«
        if (debugGun != null)
        {
            mainWeapon = Instantiate(debugGun, PC.transform);

        }
        else
            mainWeapon = Instantiate(PC.Inventory.mainWeapon, PC.transform);
        gun = mainWeapon.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PC.stateMachine.CurrentState.GetCanShot())
            CheckShot();
    }

    private void CheckShot()
    {
        if (PC.inputController.ShotInput)
        {
            if (gun != null)
            {
                gun.Shot();
                if (!gun.GetFullAuto())
                    PC.inputController.UseShotInput();
            }
            else
                Debug.Log("Not Set Gun!!");
        }
    }
}
