using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashPanel : MonoBehaviour
{
    private Image flashPanel;
    private Core core;

    private States States { get => states ?? core.GetCoreComponent(ref states); }
    private States states;

    private bool nowFlashEffect;

    private void Start()
    {
        flashPanel = GetComponent<Image>();
        core = transform.root.gameObject.GetComponentInChildren<Core>();
        nowFlashEffect = false;
    }

    private void Update()
    {
        if(!nowFlashEffect && States.nowWeakening == States.WeakeningState.FrashBang)
        {
            //フラッシュを受けた際の処理
            nowFlashEffect = true;
            Color color = flashPanel.color;
            color.a = 1.0f;
            flashPanel.color = color;
        }

        if(nowFlashEffect)
        {
            float BlindTime = States.weakeningHoldTime * 0.8f;
            if(States.weakeningStartTime + BlindTime < Time.time)
            {
                //徐々にフラッシュ効果が消える処理
                Color color = flashPanel.color;
                color.a = Animation(States.weakeningStartTime + BlindTime, States.weakeningStartTime + States.weakeningHoldTime, 1.0f, 0.0f, Time.time);
                flashPanel.color = color;
                if (color.a <= 0)
                    nowFlashEffect = false;
            }
        }
    }

    private float Animation(float startTime, float endTime, float startKey, float endKey, float nowTime)
    {
        float t = (endTime - startTime);
        float p = (nowTime - startTime) / t;

        float k = (endKey - startKey);
        float key = startKey + (k * p);

        return key;
    }
}
