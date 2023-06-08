using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gadgetSelect : MonoBehaviour
{
    [SerializeField] private GameObject gadgetPrefab;
    [SerializeField] private Color notSelectColor;
    [SerializeField] private Color selectColor;
    [SerializeField] private Image buttonImage;
    public static List<GameObject> setGadgetObject = new List<GameObject>();
    private bool isSelect;

    public void Start()
    {
        isSelect = false;
        buttonImage.color = notSelectColor;
        setGadgetObject.Clear();
    }

    public void OnClickGadgetSelect()
    {
        if (!isSelect)
        {
            isSelect = true;
            buttonImage.color = selectColor;
            SetGadget();
        }
        else
        {
            isSelect = false;
            buttonImage.color = notSelectColor;
            DelGadget();
        }
    }

    private void SetGadget()
    {
        setGadgetObject.Add(gadgetPrefab);
    }

    private void DelGadget()
    {
        setGadgetObject.Remove(gadgetPrefab);
    }
}
