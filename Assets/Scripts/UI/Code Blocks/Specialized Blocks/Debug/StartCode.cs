using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartCode : Code, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Continue();
    }
}
