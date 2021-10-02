using System;
using System.Collections.Generic;
using UnityEngine;

public class LatentAction
{
    private event Action _action;
    private bool _hasBeenCalled;

    public LatentAction()
    {
        _hasBeenCalled = false;
    }

    public void AddListener(Action action)
    {
        _action += action;
        if (_hasBeenCalled) action();
    }

    public void Invoke()
    {
        _hasBeenCalled = true;
        EventUtil.SafeInvoke(_action);
    }
}
