using UnityEngine;

// https://www.youtube.com/watch?v=HXaFLm3gQws
public class DontDestroyOnLoad : MonoBehaviour
{
    private string objectID;

    private void Awake()
    {
        objectID = name;
    }

    void Start()
    {
        // Object.FindObjectOfType<DontDestroyOnLoad>()
        var objects = FindObjectsOfType<DontDestroyOnLoad>();
        for (int i = 0; i < objects.Length; ++i)
        {
            if (objects[i] != this && objects[i].objectID == objectID)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}
