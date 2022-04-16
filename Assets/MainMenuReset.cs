using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuReset : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
    }
}
