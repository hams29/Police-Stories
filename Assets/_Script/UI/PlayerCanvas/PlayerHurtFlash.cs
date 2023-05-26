using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHurtFlash : MonoBehaviour
{
    [SerializeField]
    private float time1 = 1.0f;
    [SerializeField]
    private float time2 = 0.5f;
    [SerializeField]
    private Vector3 ColorCode;
    [SerializeField]
    private float maxAlpha = 0.5f;

    private float maxHP;
    private float currentHP;
    private float startTime;
    private float nowTime;
    private bool isBlind;
    private bool isStart;
    private Image image;

    private void Start()
    {
        nowTime = Time.time;
        image = GetComponent<Image>();
        isBlind = false;
        isStart = false;
    }

    private void Update()
    {
        if(currentHP <= 0)
        {
            image.color = new Color(ColorCode.x, ColorCode.y, ColorCode.z, maxAlpha);
            return;
        }

        nowTime = Time.time;

        if (currentHP < maxHP * 0.3f)
        {
            if(!isStart)
            {
                isStart = true;
                startTime = Time.time;
            }

            if (!isBlind)
            {
                float a = Animation(startTime, startTime + time2, 0, maxAlpha, nowTime);
                image.color = new Color(ColorCode.x, ColorCode.y, ColorCode.z, a);

                if (a >= maxAlpha)
                {
                    isBlind = true;
                    isStart = false;
                }
            }
            else
            {
                float a = Animation(startTime, startTime + time2, maxAlpha, 0f, nowTime);
                image.color = new Color(ColorCode.x, ColorCode.y, ColorCode.z, a);

                if (a <= 0)
                {
                    isBlind = false;
                    isStart = false;
                }
            }
        }
        else if (currentHP < maxHP * 0.5f)
        {
            if (!isStart)
            {
                isStart = true;
                startTime = Time.time;
            }

            if (!isBlind)
            {
                float a = Animation(startTime, startTime + time1, 0, maxAlpha, nowTime);
                image.color = new Color(ColorCode.x, ColorCode.y, ColorCode.z, a);

                if (a >= maxAlpha)
                {
                    isBlind = true;
                    isStart = false;
                }
            }
            else
            {
                float a = Animation(startTime, startTime + time1, maxAlpha, 0f, nowTime);
                image.color = new Color(ColorCode.x, ColorCode.y, ColorCode.z, a);

                if (a <= 0)
                {
                    isBlind = false;
                    isStart = false;
                }
            }
        }
        else
            isStart = false;
    }

    private float Animation(float startTime,float endTime,float startKey,float endKey,float nowTime)
    {
        float t = (endTime - startTime);
        float p = (nowTime - startTime) / t;

        float k = (endKey - startKey);
        float key = startKey + (k * p);

        return key;
    }

    public void SetMaxHP(float max) { maxHP = max; }
    public void SetCurrentHP(float current) { currentHP = current; }
}
