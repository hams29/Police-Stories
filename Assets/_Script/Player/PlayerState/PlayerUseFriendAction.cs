using UnityEngine;

public class PlayerUseFriendAction : PlayerState
{
    Vector2 mousePosition;
    Vector3 position;
    Vector3 doorPosition;
    public PlayerUseFriendAction(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName):base(player,stateMachine,playerData,animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        if(gameManager.GameManager == null) { stateMachine.ChangeState(player.IdleState); return; }
        if(gameManager.GameManager.friend == null) { stateMachine.ChangeState(player.IdleState); return; }

        mousePosition = player.inputController.MousePosition;
        player.friendActionUI.AddSendAction(FriendController.SendAction.Move);
        FriendController friend = gameManager.GameManager.friend.GetComponent<FriendController>();
        if(friend.isFollow)
        {
            //味方NPCがフォロー中の場合
            player.friendActionUI.AddSendAction(FriendController.SendAction.Stop);
        }
        else
        {
            //味方NPCが止まっている場合
            player.friendActionUI.AddSendAction(FriendController.SendAction.Follow);
        }

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "Door")
            {
                player.friendActionUI.AddSendAction(FriendController.SendAction.OpenDoor);
                player.friendActionUI.AddSendAction(FriendController.SendAction.ThrowFrashBang);
                doorPosition = hit.transform.GetComponent<doorVariable>().doorCameraPos.transform.position;
                //doorPosition = hit.transform.position;
            }
        }

        //マウスが指定するワールド座標の取得
        Plane plane = new Plane();
        float distance = 0;
        ray = Camera.main.ScreenPointToRay(player.inputController.MousePosition);
        plane.SetNormalAndPosition(Vector3.up, player.transform.localPosition);
        if (plane.Raycast(ray, out distance))
        {
            position = ray.GetPoint(distance);
        }

        player.friendActionUI.SetShow(true);
        Movement?.SetVelocityZero();
        Rotation.CanSetRotate = false;
        Movement.CanSetVelocity = false;
    }

    public override void Exit()
    {
        base.Exit();

        player.friendActionUI.SetShow(false);
        Rotation.CanSetRotate = true;
        Movement.CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //InventoryInputがfalseになったとき
        if (!friendActionInput)
        {
            //player.inventoryUI.HideInventoryUI();

            if (player.friendActionUI.isSelect)
            {
                if(player.friendActionUI.selectAction == FriendController.SendAction.OpenDoor)
                    gameManager.GameManager.friend.SetTurgetPosition(doorPosition);
                else
                    gameManager.GameManager.friend.SetTurgetPosition(position);

                gameManager.GameManager.friend.ReceiveAction(player.friendActionUI.selectAction);
            }

            player.friendActionUI.UseIsSelect();
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private bool CheckHaveGadget(int gadgetNumber)
    {
        bool re = false;
        int gadCount = player.Inventory.gadgets.Count;

        if (gadgetNumber <= gadCount)
            re = true;


        return re;
    }
}
