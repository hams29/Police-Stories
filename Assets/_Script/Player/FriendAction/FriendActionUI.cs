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

    [Header("�}�E�X�𓮂����Ă��������Ȃ��͈�")]
    [SerializeField]
    private float deadZone;

    [Header("�t���[���ɂȂ�UI�̃I�u�W�F�N�g")]
    [SerializeField]
    DirectionUI directionUIFrame;

    [Header("�A�C�R���ɂȂ�UI�̃I�u�W�F�N�g")]
    [SerializeField]
    private DirectionUI directionUIIcon;

    [Header("�I���A��I�����̃J���[")]
    [SerializeField]
    private Color selectColor;
    [SerializeField]
    private Color defaultColor;

    [Header("�A�C�R���\�����̒�������̃X�y�[�X")]
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
            //�\�������鏈��
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
            //UI�̔�\������
            nowShow = false;
            SetActiveGameObject(directionUIFrame, false);
            SetActiveGameObject(directionUIIcon, false);
        }

        //UI���\�����̎�
        if(isShow)
        {
            //TODO::FriendActionUI::�K�W�F�b�g���I�����ꂽ�ۂɉ����I�����ꂽ���킩����̂̒ǉ�
            //�}�E�X���E�ɓ������Ƃ�
            if(mousePos.x > oldMousePos.x + deadZone)
            {
                //�E�ɓ������Ƃ��̏���
                for(int i = 0;i<directionUIFrame.Count;i++)
                {
                    SetObjectImageColor(directionUIFrame[i], defaultColor);
                }
                SetObjectImageColor(directionUIFrame.right, selectColor);
                isSelect = true;
                selectAction = Action.ActionRight;
                
            }
            //�}�E�X�����ɓ������Ƃ�
            else if(mousePos.x < oldMousePos.x - deadZone)
            {
                //���ɓ������Ƃ��̏���
                for (int i = 0; i < directionUIFrame.Count; i++)
                {
                    SetObjectImageColor(directionUIFrame[i], defaultColor);
                }
                SetObjectImageColor(directionUIFrame.left, selectColor);
                isSelect = true;
                selectAction = Action.ActionLeft;
            }
            //�}�E�X����ɓ������Ƃ�
            else if(mousePos.y > oldMousePos.y + deadZone)
            {
                //��ɓ������Ƃ��̏���
                for (int i = 0; i < directionUIFrame.Count; i++)
                {
                    SetObjectImageColor(directionUIFrame[i], defaultColor);
                }
                SetObjectImageColor(directionUIFrame.up, selectColor);
                isSelect = true;
                selectAction = Action.ActionUp;
            }
            //�}�E�X�����ɓ������Ƃ�
            else if(mousePos.y < oldMousePos.y - deadZone)
            {
                //���ɓ������Ƃ��̏���
                for (int i = 0; i < directionUIFrame.Count; i++)
                {
                    SetObjectImageColor(directionUIFrame[i], defaultColor);
                }
                SetObjectImageColor(directionUIFrame.down, selectColor);
                isSelect = true;
                selectAction = Action.ActionDown;
            }
            //�����Ȃ������Ƃ��̏���
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
            Debug.Log(obj.name + "��Image�R���|�[�l���g�����݂��܂���B");
            return;
        }

        img.color = color;
    }

    private void SetObjectRectTransformPosition(GameObject obj, Vector2 mPos)
    {
        RectTransform rTran = obj.GetComponent<RectTransform>();
        if (rTran == null)
        {
            Debug.Log(obj.name + "��RectTransform�R���|�[�l���g�����݂��܂���B");
            return;
        }

        rTran.position = mPos;
    }

    private void SetObjectRectTransformPosition(GameObject obj, Vector2 mPos, Vector2 addPos)
    {
        RectTransform rTran = obj.GetComponent<RectTransform>();
        if (rTran == null)
        {
            Debug.Log(obj.name + "��RectTransform�R���|�[�l���g�����݂��܂���B");
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

    //UI��\������ۂɕ\��������A�N�V�����̒ǉ�
    public void AddSendAction(FriendController.SendAction action)
    {
        for(int i = 0;i<sendAction.Count;i++)
        {
            if (sendAction[i] != FriendController.SendAction.None)
                continue;

            sendAction[i] = action;
        }
    }

    //UI�������ۂɃA�N�V�������Z�b�g�p�֐�
    public void ReomveSendAction()
    {
        for(int i = 0;i<sendAction.Count;i++)
        {
            sendAction[i] = FriendController.SendAction.None;
        }
    }
}
