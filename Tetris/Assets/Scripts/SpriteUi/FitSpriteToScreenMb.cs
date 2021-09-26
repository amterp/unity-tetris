using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitSpriteToScreenMb : MonoBehaviour
{

    public Camera PlayerCamera;

    private FitSpriteToScreen _fitSpriteToScreen;

    // Start is called before the first frame update
    void Start()
    {
        _fitSpriteToScreen = new FitSpriteToScreen(PlayerCamera, GetComponent<SpriteRenderer>());
        _fitSpriteToScreen.StretchToScreen(transform);
    }

    void Update()
    {
        _fitSpriteToScreen.StretchToScreen(transform);
    }
}
