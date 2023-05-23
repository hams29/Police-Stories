using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{
    public enum Scene
    {
        Title,
        StageSelect,
        GunSet,
        Game,
        GameClear,
        GameOver
    }
    public class nextSetGun
    {
        public GameObject gun;
    }

    public static gameManager GameManager;


    private Text remainingAmmoText;
    private Text scoreText;
    private Text maxAmmoText;

    private Slider magazinSlider1;
    private Slider magazinSlider2;
    private Slider magazinSlider3;

    private Image currentNPCStateImage;

    public Gun gun { get; private set; }
    private float score;
    private Scene nowScene;

    private bool isSetGun;
    private bool isSetGameUI;

    public nextSetGun setGun;
    private void Awake()
    {
        
        if (GameManager == null)
            GameManager = this;
        else
            Destroy(this);

        //NPCの現在の状態の画像を入れる。
        currentNPCStateImage = null;
        score = 0;
        nowScene = Scene.Title;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        isSetGun = false;
        isSetGameUI = false;
    }

    public void SetPlayerGun(Gun gunScript) 
    {
        this.gun = gunScript;
        isSetGun = true;
        if(isSetGameUI)
        {
            maxAmmoText.text = (!gun) ? "" : gun.GetMainWeaponData().maxAmmo.ToString();
            magazinSlider1.maxValue = gun.GetMainWeaponData().maxAmmo;
            magazinSlider2.maxValue = gun.GetMainWeaponData().maxAmmo;
            magazinSlider3.maxValue = gun.GetMainWeaponData().maxAmmo;
        }
    }

    public void Update()
    {
        if(nowScene == Scene.Game && isSetGun && isSetGameUI)
        {
            remainingAmmoText.text = (!gun) ? "" : gun.GetCurrentMagazineAmmo().ToString();
            magazinSlider1.value = (!gun) ? 0 : gun.currentMagazine[0];
            magazinSlider2.value = (!gun) ? 0 : gun.currentMagazine[1];
            magazinSlider3.value = (!gun) ? 0 : gun.currentMagazine[2];
        }
    }

    public void SetGameUI(Text RemainingAmmo,Text Score,Text MaxAmmo, Slider Magazin1,Slider Magazin2,Slider Magazin3 ,Image CurrentNPCState)
    {
        this.remainingAmmoText = RemainingAmmo;
        this.scoreText = Score;
        this.maxAmmoText = MaxAmmo;
        this.magazinSlider1 = Magazin1;
        this.magazinSlider2 = Magazin2;
        this.magazinSlider3 = Magazin3;
        this.currentNPCStateImage = CurrentNPCState;
        scoreText.text = score.ToString();
        isSetGameUI = true;
        if (isSetGun)
        {
            maxAmmoText.text = (!gun) ? "" : gun.GetMainWeaponData().maxAmmo.ToString();
            magazinSlider1.maxValue = gun.GetMainWeaponData().maxAmmo;
            magazinSlider2.maxValue = gun.GetMainWeaponData().maxAmmo;
            magazinSlider3.maxValue = gun.GetMainWeaponData().maxAmmo;
        }
    }

    public void ResetScore() { score = 0; }
    public void AddScore(float addScore) { this.score += addScore; }
    public void SetNextScene(Scene ns) 
    {
        switch(nowScene)
        {
            case Scene.Game:
                isSetGun = false;
                isSetGameUI = false;
                break;
        }
        nowScene = ns; 
    }
}
