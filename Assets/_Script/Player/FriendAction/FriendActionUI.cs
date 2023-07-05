using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendActionUI : MonoBehaviour
{
    public enum Action
    {
        Action1,
        Action2,
        Action3,
        Action4,
    };

    [System.Serializable]
    struct DirectionUI
    {
        public GameObject left;
        public GameObject right;
        public GameObject up;
        public GameObject down;

        public GameObject this[int dir]
        {
            get
            {
                switch (dir)
                {
                    case 0:
                        return left;
                    case 1:
                        return right;
                    case 2:
                        return up;
                    case 3:
                        return down;
                }
                return null;
            }
        }

        public int Count
        {
            get
            {
                return 4;
            }
        }
    }

    public bool isShow { get; private set; }
    public bool isSelect { get; private set; }
    public Action selectAction { get; private set; }

    private bool nowShow;

    private Vector2 mousePos;
    private Vector2 oldMousePos;

    [Header("マウスを動かしても反応しない範囲")]
    [SerializeField]
    private float deadZone;

    [Header("フレームになるUIのオブジェクト")]
    [SerializeField]
    DirectionUI directionUIFrame;

    [Header("アイコンになるUIのオブジェクト")]
    [SerializeField]
    private DirectionUI directionUIIcon;

    [Header("選択、非選択時のカラー")]
    [SerializeField]
    private Color selectColor;
    [SerializeField]
    private Color defaultColor;

    [Header("アイコン表示時の中央からのスペース")]
    [SerializeField]
    private float spriteSpace = 10.0f;

    private void Start()
    {
        isShow = false;
        isSelect = false;
        nowShow = false;
        selectAction = Action.Action1;

        SetActiveGameObject(directionUIFrame, false);
        SetActiveGameObject(directionUIIcon, false);
    }

    private void Update()
    {
        if (isShow && !nowShow)
        {
            //表示させる処理
            nowShow = true;
            isSelect = false;
            SetActiveGameObject(directionUIFrame, true);
            SetActiveGameObject(directionUIIcon, true);

            for(int i = 0;i<directionUIFrame.Count;i++)
            {
                SetObjectRectTransformPosition(directionUIFrame[i], mousePos);
                SetObjectImageColor(directionUIIcon[i], defaultColor);
            }

            //TODO::setObjectRectTransformPositionの続き
            SetObjectRectTransformPosition(directionUIIcon.up, mousePos, new Vector3(0, spriteSpace, 0));
        }
    }

    private void SetObjectImageColor(GameObject obj, Color color)
    {
        Image img = obj.GetComponent<Image>();
        if (img == null)
        {
            Debug.Log(obj.name + "にImageコンポーネントが存在しません。");
            return;
        }

        img.color = color;
    }

    private void SetObjectRectTransformPosition(GameObject obj, Vector3 mPos)
    {
        RectTransform rTran = obj.GetComponent<RectTransform>();
        if (rTran == null)
        {
            Debug.Log(obj.name + "にRectTransformコンポーネントが存在しません。");
            return;
        }

        rTran.position = mPos;
    }

    private void SetObjectRectTransformPosition(GameObject obj, Vector3 mPos, Vector3 addPos)
    {
        RectTransform rTran = obj.GetComponent<RectTransform>();
        if (rTran == null)
        {
            Debug.Log(obj.name + "にRectTransformコンポーネントが存在しません。");
            return;
        }

        rTran.position = mPos;
    }

    private void SetActiveGameObject(DirectionUI directionUI,bool isActive)
    {
        for(int i = 0;i<directionUI.Count;i++)
        {
            directionUI[i].SetActive(isActive);
        }
    }
}
