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

    [SerializeField]
    private Image haveInventoryImage;

    [SerializeField]
    private GameObject InventoryUITopImg;
    [SerializeField]
    private GameObject InventoryUIBottomImg;
    [SerializeField]
    private GameObject InventoryUILeftImg;
    [SerializeField]
    private GameObject InventoryUIRightImg;

    private Image InventoryUITopImage;
    private Image InventoryUIBottomImage;
    private Image InventoryUILeftImage;
    private Image InventoryUIRightImage;

    private RectTransform InventoryUITopPos;
    private RectTransform InventoryUIBottomPos;
    private RectTransform InventoryUILeftPos;
    private RectTransform InventoryUIRightPos;

    private List<GameObject> allUIObject = new List<GameObject>();

    private Inventory inventory;

    [SerializeField]
    private Color selectColor;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private float spriteSpace = 10.0f;

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



        allUIObject.Add(InventoryUITopObj);
        allUIObject.Add(InventoryUIBottomObj);
        allUIObject.Add(InventoryUILeftObj);
        allUIObject.Add(InventoryUIRightObj);

        allUIObject.Add(InventoryUITopImg);
        allUIObject.Add(InventoryUIBottomImg);
        allUIObject.Add(InventoryUILeftImg);
        allUIObject.Add(InventoryUIRightImg);

        foreach(GameObject obj in allUIObject)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        if(isShow && !nowShow)
        {
            nowShow = true;
            isSelect = false;
            foreach (GameObject obj in allUIObject)
            {
                obj.SetActive(true);
            }

            InventoryUITopPos.position = mousePos;
            InventoryUIBottomPos.position = mousePos;
            InventoryUILeftPos.position = mousePos;
            InventoryUIRightPos.position = mousePos;

            InventoryUITopImage.color = defaultColor;
            InventoryUIBottomImage.color = defaultColor;
            InventoryUILeftImage.color = defaultColor;
            InventoryUIRightImage.color = defaultColor;

            InventoryUITopImg.GetComponent<RectTransform>().position = new Vector2(mousePos.x, mousePos.y + spriteSpace);
            InventoryUIBottomImg.GetComponent<RectTransform>().position = new Vector2(mousePos.x, mousePos.y - spriteSpace);
            InventoryUIRightImg.GetComponent<RectTransform>().position = new Vector2(mousePos.x + spriteSpace, mousePos.y);
            InventoryUILeftImg.GetComponent<RectTransform>().position = new Vector2(mousePos.x - spriteSpace, mousePos.y);

            oldMousePos = mousePos;
        }   
        else if(!isShow && nowShow)
        {
            nowShow = false;
            foreach (GameObject obj in allUIObject)
            {
                obj.SetActive(false);
            }
        }

        if(isShow)
        {
            //マウスが右に動いたとき
            if (mousePos.x > oldMousePos.x + deadZone)
            {
                InventoryUITopImage.color = defaultColor;
                InventoryUIBottomImage.color = defaultColor;
                InventoryUILeftImage.color = defaultColor;
                InventoryUIRightImage.color = selectColor;

                selectItem = Item.gadget2;
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

                selectItem = Item.mainWeapon;
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
    public void SetInventory(Inventory inv)
    {
        inventory = inv;
        if (inventory.mainWeapon != null)
        {
            InventoryUITopImg.GetComponent<Image>().sprite = inventory.mainWeaponTable.gunSprite;
            SetHaveInventoryImage(inventory.mainWeaponTable.gunSprite);
        }

        Image left = InventoryUILeftImg.GetComponent<Image>();
        Image right = InventoryUIRightImg.GetComponent<Image>();
        Image bottom = InventoryUIBottomImg.GetComponent<Image>();

        if (inventory.gadgetTables.Count > 0)
        {
            for(int i = 0;i<inventory.gadgetTables.Count;i++)
            {
                switch(i)
                {
                    case 0:
                        left.sprite = inventory.gadgetTables[i].gadgetImage;
                        break;

                    case 1:
                        right.sprite = inventory.gadgetTables[i].gadgetImage;
                        break;

                    case 2:
                        bottom.sprite = inventory.gadgetTables[i].gadgetImage;
                        break;

                    default:
                        break;
                }
            }
        }

        if (left.sprite == null)
            left.color = new Color(0, 0, 0, 0);
        if (right.sprite == null)
            right.color = new Color(0, 0, 0, 0);
        if (bottom.sprite == null)
            bottom.color = new Color(0, 0, 0, 0);
    }

    public void SetHaveInventoryImage(Sprite img)
    {
        haveInventoryImage.sprite = img;
    }
}
