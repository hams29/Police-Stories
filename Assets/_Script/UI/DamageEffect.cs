using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour 
{
    [SerializeField] private CanvasGroup playerHurt;

    public void PlayerDamagedHurt()
    {
        if (playerHurt.alpha < 1)
        {
            playerHurt.alpha += 0.25f;
        }
    }
}
