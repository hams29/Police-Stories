using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendActionUI : MonoBehaviour
{
    public enum Action
    {
        ActionUp,
        ActionDown,
        ActionRight,
        ActionLeft,
        ActionNone
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

        public int Count { get => 4; }
    }
    struct DirectionSendAction
    {
        public FriendController.SendAction actionUp;
        public FriendController.SendAction actionDown;
        public FriendController.SendAction actionRight;
        public FriendController.SendAction actionLeft;

        public FriendController.SendAction this[int dir]
        {
            get
            {
                switch(dir)
                {
                    case 0:
                        return actionUp;
                    case 1:
                        return actionDown;
                    case 2:
                        return actionLeft;
                    case 3:
                        return actionRight;
                }
                return FriendController.SendAction.None;
            }
            set
            {
                switch (dir)
                {
                    case 0:
                        actionUp = value;
                        break;
                    case 1:
                        actionDown = value;
                        break;
                    case 2:
                        actionLeft = value;
                        break;
                    case 3:
                        actionRight = value;
                        break;
                }
            }
        }

        public int Count { get => 4; }
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

    private DirectionSendAction sendAction;

    private void Start()
    {
        isShow = false;
        isSelect = false;
        nowShow = false;
        selectAction = Action.ActionNone;

        SetActiveGameObject(directionUIFrame, false);
        SetActiveGameObject(directionUIIcon, false);

        for (int i = 0; i < sendAction.Count; i++)
        {
            sendAction[i] = FriendController.SendAction.None;
        }
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

            SetObjectRectTransformPosition(directionUIIcon.up, mousePos, new Vector2(0,spriteSpace));
            SetObjectRectTransformPosition(directionUIIcon.down, mousePos, new Vector2(0,-spriteSpace));
            SetObjectRectTransformPosition(directionUIIcon.right, mousePos, new Vector2(spriteSpace, 0));
            SetObjectRectTransformPosition(directionUIIcon.left, mousePos, new Vector2(-spriteSpace, 0));

            oldMousePos = mousePos;
        }
        else if(!isShow && nowShow)
        {
            //UIの非表示処理
            nowShow = false;
            SetActiveGameObject(directionUIFrame, false);
            SetActiveGameObject(directionUIIcon, false);
        }

        //UIが表示中の時
        if(isShow)
        {
            //TODO::FriendActionUI::ガジェットが選択された際に何が選択されたかわかるものの追加
            //マウスが右に動いたとき
            if(mousePos.x > oldMousePos.x + deadZone)
            {
                //右に動いたときの処理
                for(int i = 0;i<directionUIFrame.Count;i++)
                {
                    SetObjectImageColor(directionUIFrame[i], defaultColor);
                }
                SetObjectImageColor(directionUIFrame.right, selectColor);
                isSelect = true;
                selectAction = Action.ActionRight;
                
            }
            //マウスが左に動いたとき
            else if(mousePos.x < oldMousePos.x - deadZone)
            {
                //左に動いたときの処理
                for (int i = 0; i < directionUIFrame.Count; i++)
                {
                    SetObjectImageColor(directionUIFrame[i], defaultColor);
                }
                SetObjectImageColor(directionUIFrame.left, selectColor);
                isSelect = true;
                selectAction = Action.ActionLeft;
            }
            //マウスが上に動いたとき
            else if(mousePos.y > oldMousePos.y + deadZone)
            {
                //上に動いたときの処理
                for (int i = 0; i < directionUIFrame.Count; i++)
                {
                    SetObjectImageColor(directionUIFrame[i], defaultColor);
                }
                SetObjectImageColor(directionUIFrame.up, selectColor);
                isSelect = true;
                selectAction = Action.ActionUp;
            }
            //マウスが下に動いたとき
            else if(mousePos.y < oldMousePos.y - deadZone)
            {
                //下に動いたときの処理
                for (int i = 0; i < directionUIFrame.Count; i++)
                {
                    SetObjectImageColor(directionUIFrame[i], defaultColor);
                }
                SetObjectImageColor(directionUIFrame.down, selectColor);
                isSelect = true;
                selectAction = Action.ActionDown;
            }
            //動かなかったときの処理
            else
            {
                for (int i = 0; i < directionUIFrame.Count; i++)
                {
                    SetObjectImageColor(directionUIFrame[i], defaultColor);
                }
                isSelect = false;
                selectAction = Action.ActionNone;
            }
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

    private void SetObjectRectTransformPosition(GameObject obj, Vector2 mPos)
    {
        RectTransform rTran = obj.GetComponent<RectTransform>();
        if (rTran == null)
        {
            Debug.Log(obj.name + "にRectTransformコンポーネントが存在しません。");
            return;
        }

        rTran.position = mPos;
    }

    private void SetObjectRectTransformPosition(GameObject obj, Vector2 mPos, Vector2 addPos)
    {
        RectTransform rTran = obj.GetComponent<RectTransform>();
        if (rTran == null)
        {
            Debug.Log(obj.name + "にRectTransformコンポーネントが存在しません。");
            return;
        }

        rTran.position = mPos + addPos;
    }

    private void SetActiveGameObject(DirectionUI directionUI,bool isActive)
    {
        for(int i = 0;i<directionUI.Count;i++)
        {
            directionUI[i].SetActive(isActive);
        }
    }

    //UIを表示する際に表示させるアクションの追加
    public void AddSendAction(FriendController.SendAction action)
    {
        for(int i = 0;i<sendAction.Count;i++)
        {
            if (sendAction[i] != FriendController.SendAction.None)
                continue;

            sendAction[i] = action;
        }
    }

    //UIを消す際にアクションリセット用関数
    public void ReomveSendAction()
    {
        for(int i = 0;i<sendAction.Count;i++)
        {
            sendAction[i] = FriendController.SendAction.None;
        }
    }
}
