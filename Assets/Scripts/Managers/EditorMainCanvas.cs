using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMainCanvas : MonoBehaviour
{
    public static EditorMainCanvas instance;
    private void Awake() {
        instance = this;
    }
}
