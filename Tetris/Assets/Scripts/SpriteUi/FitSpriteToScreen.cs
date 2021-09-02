using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitSpriteToScreen
{
    private Camera _playerCamera;
    private SpriteRenderer _spriteRenderer;

    public FitSpriteToScreen(Camera playerCamera, SpriteRenderer spriteRenderer)
    {
        _playerCamera = playerCamera;
        _spriteRenderer = spriteRenderer;
    }

    public void StretchToScreen(Transform transform)
    {
        float spriteHeight = _spriteRenderer.sprite.bounds.size.y;
        float spriteWidth = _spriteRenderer.sprite.bounds.size.x;

        float aspectRatio = Screen.width / (float)Screen.height;
        float worldScreenHeight = _playerCamera.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight * aspectRatio;

        transform.localScale = new Vector3(worldScreenWidth / spriteWidth, worldScreenHeight / spriteHeight);
    }
}
