using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidee : MonoBehaviour
{

    [SerializeField] private Hider _hider;
    [SerializeField] private bool _beginHidden;
    [SerializeField] private float _unhideDelaySeconds;

    void Start()
    {
        if (_beginHidden) Hide();
    }

    public void Hide()
    {
        _hider.Hide(gameObject);
    }

    public void Unhide()
    {
        StartCoroutine(TriggerUnhide());
    }

    private IEnumerator TriggerUnhide()
    {
        yield return new WaitForSeconds(_unhideDelaySeconds);
        _hider.Unhide(gameObject);
    }
}
