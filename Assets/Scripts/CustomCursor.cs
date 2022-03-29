using System.Collections;
using System.Collections.Generic;
using Unity.CodeEditor;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    protected static CustomCursor instance;
    /*[SerializeField]
    protected Texture2D cursor;

    protected void Awake()
    {
        instance = this;
        Cursor.SetCursor(cursor, Vector3.zero, CursorMode.Auto);
    }*/

    void Update()
    {
        if (EditorController.instance.is_in_editor)
        {
            Cursor.visible = true;
            return;
        }

        Cursor.visible = false;
        if (Camera.main == null)
        {
            Debug.Log("Camera.main == null");
            return;
        }
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
