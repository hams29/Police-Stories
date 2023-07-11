using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasChange : MonoBehaviour
{


    private int canvasCount;

    private GameEndInputHandler gameEndInputHandler;

    [SerializeField] private WeaponSetSceneChange sceneChange;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Text returnTextMesh;
    [SerializeField] private Text eTextMesh;
    [SerializeField] private GameObject[] canvasGrp;


    private void Start()
    {
        canvasCount = 0;
        gameEndInputHandler = InputManagerDontDestroy.Instance.gameEndInputHandler;
    }

    private void Update()
    {
        if (canvasCount > 3)
        {
            canvasCount = 3;
        }
        else if (canvasCount < 0)
        {
            canvasCount = 0;
        }

        if (gameEndInputHandler.CanvasChangeNextInput)
        {
            gameEndInputHandler.UseNextInput();
            CountPlus();
            SceneChange();

        }
        else if(gameEndInputHandler.CanvasChangeReturnInput)
        {
            gameEndInputHandler.UseCheckReturnInput();
            CountMinus();
            OnReturnScene();
        }

             

        switch(canvasCount)
        {
            case 0:
                Mission();
                break;

            case 1:
                Briefing();
                break;

            case 2:
                Gadget();
                break;
        }

    }

    public void Mission()
    {
        /*canvasGrp[canvasCount].enabled = true;
        canvasGrp[1].enabled = false;
        canvasGrp[2].enabled = false;*/
        canvasGrp[canvasCount].SetActive(true);
        canvasGrp[1].SetActive(false);
        canvasGrp[2].SetActive(false);
        scrollbar.value = 0.0f;
        returnTextMesh.text = "<color=#e1e141>Q</color>やめる";
        eTextMesh.text = "<color=#e1e141>E</color>選択する";
    }

    public void Briefing()
    {
        /*canvasGrp[canvasCount].enabled = true;
        canvasGrp[0].enabled = false;
        canvasGrp[2].enabled = false;*/
        canvasGrp[canvasCount].SetActive(true);
        canvasGrp[0].SetActive(false);
        canvasGrp[2].SetActive(false);
        scrollbar.value = 0.25f;
        returnTextMesh.text = "<color=#e1e141>Q</color>戻る";
        eTextMesh.text = "<color=#e1e141>E</color>選択する";
    }

    public void Gadget()
    {
        /*canvasGrp[canvasCount].enabled = true;
        canvasGrp[0].enabled = false;
        canvasGrp[1].enabled = false;*/
        canvasGrp[canvasCount].SetActive(true);
        canvasGrp[0].SetActive(false);
        canvasGrp[1].SetActive(false);
        scrollbar.value = IsWeaponSetFull() ? 0.5f : 1.0f;
        returnTextMesh.text = "<color=#e1e141>Q</color>戻る";
        eTextMesh.text = IsWeaponSetFull() ? "<color=#e1e141>E</color>開始" : "<color=#e1e141>E</color>選択する";

        

    }


    public void CountPlus()
    {
        canvasCount++;
    }

    public void CountMinus()
    {
        canvasCount--;
    }

    public void SceneChange()
    {
        if (canvasCount == 3 && weaponSelect.Instance.GetWeapon() != null)
        {
            sceneChange?.ChangeClick();
        }
    }

    public void OnReturnScene()
    {
        if (canvasCount == 0)
        {
            sceneChange.SetSceneName(gameManager.Scene.Title);
        }
    }


    public bool IsWeaponSetFull()
    {
        if (weaponSelect.Instance.GetWeapon().gunPrefab != null)
        {
            return true;
        }
        //修正箇所
        return false;
    }

}
