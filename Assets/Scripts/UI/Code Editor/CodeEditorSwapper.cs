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
        /*if (buttons.transform.childCount != windows.transform.childCount) {
            Debug.LogError("There are " + buttons.transform.childCount + " buttons and " + windows.transform.childCount + " windows");
        }*/

        for (int i = 0; i < buttons.transform.childCount; i++)
        {
            // var tempButton = buttons.transform.GetChild(i).GetComponent<Button>();
            if (buttons.transform.GetChild(i).TryGetComponent(out EditorButton bn))
            {
                bn.SetSwapper(this);
            }
        }

        currentButton = buttons.transform.GetChild(0).GetComponent<EditorButton>();
        currentWindow = windows.transform.GetChild(0);

        currentButton.SelectButton();
    }

    public void SetActiveWindow(Transform window, EditorButton button)
    {
        if (currentButton != null) {
            currentButton.DeselectButton();
        }
        currentButton = button;
        currentWindow = window;
        currentWindow.SetAsLastSibling();
    }
}
