using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSwitchButtonController : MonoBehaviour
{

    public GameObject menuToEnable;
    public GameObject menuToDisable;

    public void SwitchMenu()
    {
        menuToEnable.SetActive(true);
        menuToDisable.SetActive(false);
    }
}