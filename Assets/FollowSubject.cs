using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSubject : MonoBehaviour
{
    public GameObject subject;

    private void FixedUpdate()
    {
        Vector3 subjectPos = subject.transform.position;
        transform.position = new Vector3(subjectPos.x, subjectPos.y, -10f);
    }
}
