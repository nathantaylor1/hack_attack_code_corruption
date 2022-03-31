using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeEditorSwapper : MonoBehaviour
{
    //protected Button currentButton = null;
    protected EditorButton currentButton = null;
    protected Transform currentWindow = null;
    //public Color selectedColor;
    public GameObject buttons;
    public GameObject windows;
    //ScrollRect sr;
    //public Dictionary<int, GameObject> buttonIdToCanvas = new Dictionary<int, GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        //sr = GetComponentInChildren<ScrollRect>();
        if (buttons.transform.childCount != windows.transform.childCount) {
            Debug.LogError("There are " + buttons.transform.childCount + " buttons and " + windows.transform.childCount + " windows");
        }

        for (int i = 0; i < buttons.transform.childCount; i++)
        {
            // Change selected color option for all buttons instead of in editor
            var tempButton = buttons.transform.GetChild(i).GetComponent<Button>();
            /*var newColors = tempButton.colors;
            newColors.selectedColor = selectedColor;
            tempButton.colors = newColors;*/
            if (tempButton.gameObject.TryGetComponent(out EditorButton bn))
            {
                bn.Init(this, windows.transform.GetChild(i));
            }

            //buttonIdToCanvas.Add(buttons.transform.GetChild(i).gameObject.GetInstanceID(), windows.transform.GetChild(i).gameObject);
        }

        currentButton = buttons.transform.GetChild(1).GetComponent<EditorButton>();
        currentWindow = windows.transform.GetChild(1);

        currentButton.SelectButton();
        // select the current button on first load
        /*var firstTime = currentButton;
        currentButton = null;*/
        //swap(buttons.transform.GetChild(0).GetComponent<Button>());
    }

    /*public void swap(Button buttonPressed) 
    {
        if (currentButton != buttonPressed) {
            *//*var buttonColors = buttonPressed.colors;*//*
            // old button resets
            if (currentButton) {
                *//*currentButton.colors = buttonColors;*//*
            }
            *//*buttonColors.normalColor = selectedColor;
            buttonColors.highlightedColor = selectedColor;*//*

            // new button changes
            *//*buttonPressed.colors = buttonColors;*//*
            currentButton = buttonPressed;
            var canvas = buttonIdToCanvas[buttonPressed.gameObject.GetInstanceID()];
            // old canvas moves out of view
            // currentWindow.SetActive(false);
            // new canvas comes into view
            // canvas.SetActive(true);
            canvas.transform.SetAsLastSibling();
            currentWindow = canvas;
            //sr.content = canvas.transform as RectTransform;
        }
    }*/

    public void SetActiveWindow(Transform window, EditorButton button)
    {
        currentButton.DeselectButton();
        currentButton = button;
        currentWindow = window;
        currentWindow.SetAsLastSibling();
    }
}
