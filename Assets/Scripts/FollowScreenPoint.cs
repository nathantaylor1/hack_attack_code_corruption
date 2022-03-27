using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScreenPoint : MonoBehaviour
{
    public Transform screenPoint;
    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(screenPoint.position);
        transform.position = cursorPos;
    }
}
