using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    private BlockController _blockController;

    void Awake()
    {
        _blockController = GetComponent<BlockController>();
    }

    void Update()
    {
        RunPlayerInput();
    }

    private void RunPlayerInput()
    {
        if (KeyBindingsChecker.InputRight())
        {
            _blockController.TryMove(1, 0);
            return;
        }
        if (KeyBindingsChecker.InputLeft())
        {
            _blockController.TryMove(-1, 0);
            return;
        }
        if (KeyBindingsChecker.InputDown())
        {
            _blockController.TryMove(0, 1);
            return;
        }
        if (KeyBindingsChecker.InputUp())
        {
            _blockController.TryRotate(RotationDirection.Clockwise);
            return;
        }
    }
}
