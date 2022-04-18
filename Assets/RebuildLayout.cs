using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebuildLayout : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait() {
        yield return new WaitForSecondsRealtime(.3f);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform); 

    }
}
