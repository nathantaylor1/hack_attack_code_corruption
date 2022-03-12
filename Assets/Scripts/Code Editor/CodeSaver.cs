using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSaver : MonoBehaviour
{
    [SerializeField]
    private GameManager.FunctionOption _selectedFunction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            save();
            EditorController.instance.safeToClose();
        }
    }
    public void save() {
        //get Start block
        foreach (Transform child in transform)
        {
            var start = child.GetComponent<StartCode>();
            if (start)
            {
                GameManager.instance.codeToRun[_selectedFunction] = start; 
            }
        }
    }
}
