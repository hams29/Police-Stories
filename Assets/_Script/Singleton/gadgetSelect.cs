using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gadgetSelect : MonoBehaviour
{
    public static List<GadgetTable> setGadgetTable = new List<GadgetTable>();

    [SerializeField] private GadgetTable gadgetPrefab;
    [SerializeField] private Color notSelectColor;
    [SerializeField] private Color selectColor;
    [SerializeField] private Image buttonImage;
    private bool isSelect;

    public void Start()
    {
        isSelect = false;
        buttonImage.color = notSelectColor;
        setGadgetTable.Clear();
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
        setGadgetTable.Add(gadgetPrefab);
    }

    private void DelGadget()
    {
        setGadgetTable.Remove(gadgetPrefab);
    }
}
