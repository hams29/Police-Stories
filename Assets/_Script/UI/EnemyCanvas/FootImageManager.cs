using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FootImageManager : MonoBehaviour
{
    [SerializeField]
    private float interval = 0.2f;
    [SerializeField]
    private bool debugFlg;

    private FootImage[] footImages;
    private bool isFootImage;
    private float startTime;
    private int count;

    private void Awake()
    {

    }

    private void Update()
    {
        if(count < footImages.Length)
        {
            if(isFootImage)
            {
                if(startTime + interval <= Time.time)
                {
                    startTime = Time.time;
                    footImages[count].StartFootStep();
                    count++;
                }
            }
            else
            {
                if(startTime + interval <= Time.time)
                {
                    startTime = Time.time;
                    footImages[count].StopFootStep();
                    count++;
                }
            }
        }
    }

    public void FootImageStart()
    {
        isFootImage = true;
        startTime = Time.time - interval;
        count = 0;
    }
    public void FootImageStop()
    {
        isFootImage = false;
        startTime = Time.time - interval;
        count = 0;
    }

    public void SetFootImagePosition(Vector3 pos)
    {
        for(int i = 0;i < footImages.Length;i++)
        {
            footImages[i].SetPosition(pos);
        }
    }

    public void InitFootImagePosition(Vector3 pos)
    {
        footImages = GetComponentsInChildren<FootImage>();
        isFootImage = false;
        count = 0;

        for (int i = 0; i < footImages.Length; i++)
            footImages[i].SetPosition(pos);
    }

    private void DebugCheck()
    {
        if (debugFlg && !isFootImage)
        {
            FootImageStart();
        }
        else if (!debugFlg && isFootImage)
        {
            FootImageStop();
        }
    }
}
