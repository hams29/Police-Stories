using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField]
    private Transform targetTfm;

    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 1.5f, 1.0f);
    private Image image;

    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        myRectTfm.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);
    }

    public void Show() { image.color = new Color(255, 255, 255, 0.5f); }
    public void Hide() { image.color = new Color(255, 255, 255, 0.0f); }
}
