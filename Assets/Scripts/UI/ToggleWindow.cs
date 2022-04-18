using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWindow : MonoBehaviour
{
    public GameObject window;
    private bool isToggled;

    private void Update()
    {
        isToggled = window.activeSelf;
    }

    public void Toggle()
    {
        window.SetActive(!isToggled);
        isToggled = !isToggled;
    }
}
