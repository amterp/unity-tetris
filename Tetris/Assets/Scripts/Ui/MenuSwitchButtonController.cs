using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuSwitchButtonController : MonoBehaviour
{

    public GameObject menuToEnable;
    public GameObject menuToDisable;

    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        menuToEnable.SetActive(true);
        menuToDisable.SetActive(false);
    }
}
