using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour 
{
    Image playerHurt;
    [SerializeField]
    private Vector3 ColorCode;

    public void Start()
    {
        playerHurt = GetComponent<Image>();
        playerHurt.color = new Color(ColorCode.x, ColorCode.y, ColorCode.z,0);
    }

    public void LogicPlayerDamageUI(float maxHP,float currentHP)
    {
        playerHurt.color = new Color(ColorCode.x, ColorCode.y, ColorCode.z, 1.0f - (currentHP / maxHP));
    }
}
