using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class CheckSelectWeapon : MonoBehaviour
{
    [SerializeField]
    private WeaponSetSceneChange sceneChange;
    [SerializeField]
    List<Image> buttons = new List<Image>();
    [SerializeField]
    private float buttonFlashInterval = 0.5f;
    [SerializeField]
    private int flashMaxCount = 5;

    List<Color> sColors = new List<Color>();
    private bool flashFlg;
    private bool flashStartFlg;
    private float flashInterval;
    private float flashTime;
    private float flashStartTime;
    private int flashCount;

    private void Update()
    {
        if (flashStartFlg)
        {
            flashTime = Time.time;
            if (flashTime > flashStartTime + buttonFlashInterval)
            {
                flashCount++;
                flashStartTime = Time.time;
                Color setColor;
                if (flashFlg)
                    setColor = Color.white;
                else
                    setColor = Color.red;

                flashFlg = !flashFlg;
                foreach (Image image in buttons)
                    image.color = setColor;
            }

            if (flashCount >= flashMaxCount)
            {
                flashStartFlg = false;
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].color = sColors[i];
                }
            }
        }
    }

    public void CheckSelect()
    {
        if(sceneChange.GetWeaponset().setGun.gun == null)
        {
            flashFlg = false;
            flashTime = 0;
            flashStartTime = 0;
            flashCount = 0;
            FlashButton();
            Debug.Log("•Ší‚ð‘I‘ð‚µ‚Ä‚­‚¾‚³‚¢B");
        }
        else
        {
            sceneChange.ChangeClick();
        }
    }

    private void FlashButton()
    {
        if (buttons.Count < 0)
            return;
        else
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i] == null)
                    return;
            }
        }
        for (int i = 0; i < buttons.Count; i++)
        {
            sColors.Add(buttons[i].color);
        }
        flashStartFlg = true;
    }
}
