using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{
    public static gameManager GameManager;


    [SerializeField] private Text scoreText;
    [SerializeField] private Text remainingAmmoText;
    [SerializeField] private Text maxAmmoText;

    [SerializeField] private Slider magazinSlider1;
    [SerializeField] private Slider magazinSlider2;
    [SerializeField] private Slider magazinSlider3;

    [SerializeField] private Image currentNPCStateImage;


    public Gun gun { get; private set; }

    private void Awake()
    {
        
        if (GameManager == null)
            GameManager = this;
        else
            Destroy(this);

        scoreText.text = "";

        //NPCÇÃåªç›ÇÃèÛë‘ÇÃâÊëúÇì¸ÇÍÇÈÅB
        currentNPCStateImage = null;
        
    }

    public void SetPlayerGun(Gun gunScript) 
    {
        this.gun = gunScript;
    }

    public void Update()
    {
        remainingAmmoText.text = (!gun) ? "" : gun.GetCurrentMagazineAmmo().ToString();
        maxAmmoText.text = (!gun) ? "" : gun.GetMainWeaponData().maxAmmo.ToString();

        magazinSlider1.maxValue = gun.GetMainWeaponData().maxAmmo;
        magazinSlider2.maxValue = gun.GetMainWeaponData().maxAmmo;
        magazinSlider3.maxValue = gun.GetMainWeaponData().maxAmmo;

        magazinSlider1.value = gun.currentMagazine[0];
        magazinSlider2.value = gun.currentMagazine[1];
        magazinSlider3.value = gun.currentMagazine[2];



    }

}
