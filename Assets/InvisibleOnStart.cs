using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleOnStart : MonoBehaviour
{
    private void Awake()
    {
        if (TryGetComponent(out SpriteRenderer sr))
        {
            sr.enabled = false;
        }
    }
}
