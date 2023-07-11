using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFriendActionUITable", menuName = "Data/UI/FriendActionUITable")]
public class FriendActionUITable : ScriptableObject
{
    [Header("Move Sprite")]
    public Sprite MoveSprite;

    [Header("Follow Sprite")]
    public Sprite FollowSprite;

    [Header("Stop Sprite")]
    public Sprite StopSprite;

    [Header("Open Door Sprite")]
    public Sprite OpenDoorSprite;

    [Header("Throw Frashbang Sprite")]
    public Sprite ThrowFrashbangSprite;
}
