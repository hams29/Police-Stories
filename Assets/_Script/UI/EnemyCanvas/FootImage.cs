using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FootImage : MonoBehaviour
{
    private Image footImage;
    [SerializeField]
    private float animationTime;
    [SerializeField]
    private Vector3 maxScale;
    [SerializeField]
    private bool debugFlg;

    private bool isFootStep;
    private bool isFootStepStop;

    private Vector3 pos;

    private float startTime;
    private float nowTime;
    private void Start()
    {
        footImage = GetComponent<Image>();
        isFootStep = false;
        isFootStepStop = false;
        nowTime = 0.0f;

        footImage.rectTransform.localScale = Vector3.zero;
        footImage.color = new Color(footImage.color.r, footImage.color.g, footImage.color.b, 0.0f);
    }

    private void Update()
    {
        DebugCheck();
        nowTime = Time.time;
        if(isFootStep)
        {
            Vector3 sc = new Vector3(AnimationValue(startTime,startTime + animationTime,0.0f,maxScale.x,nowTime),
                                     AnimationValue(startTime, startTime + animationTime, 0.0f, maxScale.y, nowTime),
                                     AnimationValue(startTime, startTime + animationTime, 0.0f, maxScale.z, nowTime));
            float a = AnimationValue(startTime, startTime + animationTime, 1.0f, 0.0f, nowTime);
            footImage.rectTransform.localScale = sc;
            footImage.color = new Color(footImage.color.r, footImage.color.g, footImage.color.b, a);

            if(nowTime >= startTime + animationTime)
            {
                footImage.rectTransform.localScale = Vector3.zero;
                footImage.color = new Color(footImage.color.r, footImage.color.g, footImage.color.b, 0);
                startTime = Time.time;
            }
        }
    }

    public void StartFootStep()
    {
        isFootStep = true;
        startTime = Time.time;
    }
    public void StopFootStep() => isFootStepStop = true;

    public void SetPosition(Vector3 pos) { this.pos = pos; }

    private float AnimationValue(float startTime,float endTime,float startKey,float endKey,float nowTime)
    {
        float ret = 0.0f;
        float t = endTime - startTime;
        float k = endKey - startKey;
        if(k >= 0)
        {
            float key = k / t;
            t = nowTime - startTime;
            ret = key * t;
        }
        else
        {
            //TODO::StartKeyがEndKeyよりも小さかった場合の処理
        }

        return ret;
    }

    private void DebugCheck()
    {
        if(debugFlg)
        {
            if (!isFootStep)
                StartFootStep();
        }
        else
        {
            if (isFootStep && !isFootStepStop)
                StopFootStep();
        }
    }
}
