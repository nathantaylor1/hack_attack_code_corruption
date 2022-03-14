using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMainCanvas : MonoBehaviour
{
    public static EditorMainCanvas instance;
    private void Awake() {
        instance = this;
        // gameObject.SetActive(false);
        // Debug.Log("closing");
        // EventManager.OnCloseEditor.AddListener(DisableVisibility);
        // EventManager.OnOpenEditor.AddListener(EnableVisibility);
        // StartCoroutine(waitAFrame());
        // EnableVisibility();
    }

    // void DisableVisibility() {
    //     GetComponent<Canvas>().enabled = false;
    // }

    // void EnableVisibility() {
    //     instance.gameObject.GetComponent<Canvas>().enabled = true;
    //     Debug.Log("opened visible " + GetComponent<Canvas>().enabled);
    // }

    // IEnumerator waitAFrame() {
        // yield return new WaitForEndOfFrame();
        // yield return new WaitForEndOfFrame();
    // }
}
