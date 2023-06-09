using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    public enum Item
    {
        mainWeapon,
        gadget1,
        gadget2,
        gadget3,
    };

    public bool isShow { get; private set; }
    public bool isSelect { get; private set; }
    
    public Item selectItem { get; private set; }

    private bool nowShow;

    private Vector2 mousePos;
    private Vector2 oldMousePos;

    [SerializeField]
    private GameObject InventoryUITopObj;
    [SerializeField]
    private GameObject InventoryUIBottomObj;
    [SerializeField]
    private GameObject InventoryUILeftObj;
    [SerializeField]
    private GameObject InventoryUIRightObj;
    [SerializeField]
    private float deadZone = 10.0f;

    private Image InventoryUITopImage;
    private Image InventoryUIBottomImage;
    private Image InventoryUILeftImage;
    private Image InventoryUIRightImage;

    private RectTransform InventoryUITopPos;
    private RectTransform InventoryUIBottomPos;
    private RectTransform InventoryUILeftPos;
    private RectTransform InventoryUIRightPos;

    [SerializeField]
    private Color selectColor;
    [SerializeField]
    private Color defaultColor;

    private void Start()
    {
        isShow                   = false;
        isSelect                 = false;
        nowShow                  = false;
        selectItem               = Item.mainWeapon;

        InventoryUITopImage      = InventoryUITopObj.GetComponent<Image>();
        InventoryUIBottomImage   = InventoryUIBottomObj.GetComponent<Image>();
        InventoryUILeftImage     = InventoryUILeftObj.GetComponent<Image>();
        InventoryUIRightImage    = InventoryUIRightObj.GetComponent<Image>();

        InventoryUITopPos        = InventoryUITopObj.GetComponent<RectTransform>();
        InventoryUIBottomPos     = InventoryUIBottomObj.GetComponent<RectTransform>();
        InventoryUILeftPos       = InventoryUILeftObj.GetComponent<RectTransform>();
        InventoryUIRightPos      = InventoryUIRightObj.GetComponent<RectTransform>();

        InventoryUITopObj.gameObject.SetActive(false);
        InventoryUIBottomObj.gameObject.SetActive(false);
        InventoryUILeftObj.gameObject.SetActive(false);
        InventoryUIRightObj.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(isShow && !nowShow)
        {
            nowShow = true;
            isSelect = false;
            InventoryUITopObj.gameObject.SetActive(true);
            InventoryUIBottomObj.gameObject.SetActive(true);
            InventoryUILeftObj.gameObject.SetActive(true);
            InventoryUIRightObj.gameObject.SetActive(true);

            InventoryUITopPos.position = mousePos;
            InventoryUIBottomPos.position = mousePos;
            InventoryUILeftPos.position = mousePos;
            InventoryUIRightPos.position = mousePos;

            InventoryUITopImage.color = defaultColor;
            InventoryUIBottomImage.color = defaultColor;
            InventoryUILeftImage.color = defaultColor;
            InventoryUIRightImage.color = defaultColor;

            oldMousePos = mousePos;
        }   
        else if(!isShow && nowShow)
        {
            nowShow = false;
            InventoryUITopObj.gameObject.SetActive(false);
            InventoryUIBottomObj.gameObject.SetActive(false);
            InventoryUILeftObj.gameObject.SetActive(false);
            InventoryUIRightObj.gameObject.SetActive(false);
        }

        //TODO::PlayerInventoryUI::InventoryUIの中身
        if(isShow)
        {
            //マウスが右に動いたとき
            if (mousePos.x > oldMousePos.x + deadZone)
            {
                InventoryUITopImage.color = defaultColor;
                InventoryUIBottomImage.color = defaultColor;
                InventoryUILeftImage.color = defaultColor;
                InventoryUIRightImage.color = selectColor;

                selectItem = Item.mainWeapon;
                isSelect = true;
            }
            //マウスが左に動いたとき
            else if (mousePos.x < oldMousePos.x - deadZone)
            {
                InventoryUITopImage.color = defaultColor;
                InventoryUIBottomImage.color = defaultColor;
                InventoryUILeftImage.color = selectColor;
                InventoryUIRightImage.color = defaultColor;

                selectItem = Item.gadget1;
                isSelect = true;
            }
            //マウスが上に動いたとき
            else if(mousePos.y > oldMousePos.y + deadZone)
            {
                InventoryUITopImage.color = selectColor;
                InventoryUIBottomImage.color = defaultColor;
                InventoryUILeftImage.color = defaultColor;
                InventoryUIRightImage.color = defaultColor;

                selectItem = Item.gadget2;
                isSelect = true;
            }
            else if(mousePos.y < oldMousePos.y - deadZone)
            {
                InventoryUITopImage.color = defaultColor;
                InventoryUIBottomImage.color = selectColor;
                InventoryUILeftImage.color = defaultColor;
                InventoryUIRightImage.color = defaultColor;

                selectItem = Item.gadget3;
                isSelect = true;
            }
            else
            {
                InventoryUITopImage.color = defaultColor;
                InventoryUIBottomImage.color = defaultColor;
                InventoryUILeftImage.color = defaultColor;
                InventoryUIRightImage.color = defaultColor;

                isSelect = false;
            }
        }
    }

    public void ShowInventoryUI() => isShow = true;
    public void HideInventoryUI() => isShow = false;
    public void SetMousePosition(Vector2 mpos) => mousePos = mpos;
}
