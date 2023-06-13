using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager  : MonoBehaviour
{
    [SerializeField]
    private Scene initScene;

    private List<GameObject> canvasObj = new List<GameObject>();
    private GameObject gameClearCanvas;
    private GameObject gameOverCanvas;

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
        //public GameObject gun;
        public GunTable gunTabele;
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
    public nextSetGun setGun;
    public GunTable setGunTable;
    public List<GadgetTable> gadgetObjects = new List<GadgetTable>();
    private float score;

    private int maxEnemy;
    private int eliminateEnemy;
    private Scene nowScene;

    private bool isSetGun = false;
    private bool isSetGameUI = false;
    public bool isGameClear { get; private set; }
    public bool isPlayerDead { get; private set; }

    private void Awake()
    {
        
        if (GameManager == null)
            GameManager = this;
        else
            Destroy(this);

        //NPCの現在の状態の画像を入れる。
        currentNPCStateImage = null;
        score = 0;
        nowScene = initScene;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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
        if(nowScene == Scene.Game)
        {
            if(isSetGun && isSetGameUI)
            {
                remainingAmmoText.text = (!gun) ? "" : gun.GetCurrentMagazineAmmo().ToString();
                magazinSlider1.value = (!gun) ? 0 : gun.currentMagazine[0];
                magazinSlider2.value = (!gun) ? 0 : gun.currentMagazine[1];
                magazinSlider3.value = (!gun) ? 0 : gun.currentMagazine[2];
                scoreText.text = score.ToString();
            }

            if (maxEnemy <= eliminateEnemy && maxEnemy != 0)
            {
                Debug.Log("ステージクリア");
                isGameClear = true;
                isSetGameUI = false;
                foreach (GameObject canvas in canvasObj)
                    canvas.SetActive(false);
                gameClearCanvas?.SetActive(true); 
            }

            if (isPlayerDead)
            {
                Debug.Log("ゲームオーバー");
                isSetGameUI = false;
                foreach (GameObject canvas in canvasObj)
                    canvas.SetActive(false);
                gameOverCanvas?.SetActive(true);
            }
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
                isPlayerDead = false;
                maxEnemy = 0;
                eliminateEnemy = 0;
                isGameClear = false;
                gadgetObjects.Clear();
                break;
        }
        nowScene = ns; 
    }

    public void addMaxEnemy() { maxEnemy++; }
    public void addEliminatedEnemy() { eliminateEnemy++; }
    public void PlayerDead() { isPlayerDead = true; }
    public void SetPlayerDead(bool flg) { isPlayerDead = flg; }

    public bool GetPlayerDead() { return isPlayerDead; }

    public float GetScore() { return  score; }

    public void addCanvasObj(GameObject obj) { canvasObj.Add(obj); }
    public void setGameClearCanvas(GameObject obj) { gameClearCanvas = obj; }
    public void setGameOverCanvas(GameObject obj) { gameOverCanvas = obj; }
    public void ReloadNowScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void ResetGameScene() 
    {
        isSetGun = false;
        isSetGameUI = false;
        isPlayerDead = false;
        maxEnemy = 0;
        eliminateEnemy = 0;
        isGameClear = false;
        ResetScore();
        canvasObj.Clear(); 
    }
}
