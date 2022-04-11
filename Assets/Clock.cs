using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void FixedUpdate()
    {
        var datetime = DateTime.Now;
        string str = datetime.ToString("hh:mm tt");
        text.text = str;
    }
}
