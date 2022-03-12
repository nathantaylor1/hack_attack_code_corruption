using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeEditorSwapper : MonoBehaviour
{
    public Button currentButton;
    public GameObject currentCanvas;
    public Color selectedColor;
    public GameObject buttons;
    public GameObject canvases;
    public Dictionary<int, GameObject> buttonIdToCanvas = new Dictionary<int, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (buttons.transform.childCount != canvases.transform.childCount) {
            Debug.LogError("Buttons must map to Canvases");
        }
        

        for (int i = 0; i < buttons.transform.childCount; i++)
        {
            // Change selected color option for all buttons instead of in editor
            var tempButton = buttons.transform.GetChild(i).GetComponent<Button>();
            var newColors = tempButton.colors;
            newColors.selectedColor = selectedColor;
            tempButton.colors = newColors;

            buttonIdToCanvas.Add(buttons.transform.GetChild(i).gameObject.GetInstanceID(), canvases.transform.GetChild(i).gameObject);
        }
        
        // select the current button on first load
        var firstTime = currentButton;
        currentButton = null;
        swap(firstTime);
    }

    public void swap(Button buttonPressed) 
    {
        if (currentButton != buttonPressed) {
            var buttonColors = buttonPressed.colors;
            // old button resets
            if (currentButton) {
                currentButton.colors = buttonColors;
            }
            buttonColors.normalColor = selectedColor;
            buttonColors.highlightedColor = selectedColor;
            // new button changes
            buttonPressed.colors = buttonColors;
            currentButton = buttonPressed;

            var canvas = buttonIdToCanvas[buttonPressed.gameObject.GetInstanceID()];
            // old canvas moves out of view
            currentCanvas.GetComponent<CodeSaver>().save();
            currentCanvas.SetActive(false);
            // new canvas comes into view
            canvas.SetActive(true);
            currentCanvas = canvas;
        }
    }
}
