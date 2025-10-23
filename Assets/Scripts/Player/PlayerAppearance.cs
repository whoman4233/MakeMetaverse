using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer accessoryRenderer;

    public void Apply(PlayerData data)
    {
        if (data == null) return;

        if (bodyRenderer != null)
        {
            bodyRenderer.sprite = data.bodySprite;
            bodyRenderer.color = data.bodyColor;
        }

        if (accessoryRenderer != null)
            accessoryRenderer.sprite = data.accessorySprite;
    }
}
