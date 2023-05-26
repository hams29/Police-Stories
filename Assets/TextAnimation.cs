using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private Text messageText;

    private Color color = new Color(1f, 1f, 1f);

    private float alpha = 0.0f;

    private void Start()
    {
        color = messageText.color;
        alpha = 0.0f;
    }

    private void Update()
    {
        alpha += 0.01f;
        TextAnim();
    }

    public void TextAnim()
    {
        color.a = alpha;
        messageText.color = color;
    }
}
