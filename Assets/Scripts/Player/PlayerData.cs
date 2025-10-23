using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Appearance")]
    public Color bodyColor = Color.white;
    public Sprite bodySprite;
    public Sprite accessorySprite;

    [Header("Stats")]
    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    [Header("Meta Info")]
    public string playerName = "Player";
}