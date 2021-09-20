using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidee : MonoBehaviour
{

    [SerializeField] private Hider _hider;
    [SerializeField] private bool _beginHidden;

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
        _hider.Unhide(gameObject);
    }
}
