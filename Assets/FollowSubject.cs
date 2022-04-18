using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSubject : MonoBehaviour
{
    public GameObject subject;
    public float moveDuration = 0.5f;
    protected bool subjectIsPlayer = false;
    protected bool isFollowing = true;
    protected Vector3 checkpointPos = Vector3.zero;

    protected void Awake()
    {
        subjectIsPlayer = subject.layer == 6;
        checkpointPos = subject.transform.position;
        checkpointPos.z = -10;
        if (subjectIsPlayer)
        {
            EventManager.OnCheckpointSave.AddListener(SaveCheckpointPos);
            //EventManager.OnPlayerDeath.AddListener(HandlePlayerDeath);
        }
    }

    protected void SaveCheckpointPos(Transform pos)
    {
        checkpointPos = pos.position;
        checkpointPos.z = -10f;
    }

    public void HandlePlayerDeath()
    {
        StartCoroutine(PlayerDeathCoroutine());
    }

    protected IEnumerator PlayerDeathCoroutine()
    {
        isFollowing = false;
        Time.timeScale = 0f;

        float initialTime = Time.unscaledTime;
        Vector3 initialPos = transform.position;

        float progress = (Time.unscaledTime - initialTime) / moveDuration;
        float smoothProgress = Mathf.Sin(Mathf.PI * progress - Mathf.PI / 2) / 2 + 0.5f;
        Vector3 returnVector = checkpointPos - transform.position;
        //returnVector.z = -10f;

        while (progress < 1.0f)
        {
            progress = (Time.unscaledTime - initialTime) / moveDuration;
            if (progress > 1.0f) break;

            smoothProgress = Mathf.Sin(Mathf.PI * progress - Mathf.PI / 2) / 2 + 0.5f; 
            Vector3 newPos = initialPos + returnVector * smoothProgress;
            transform.position = newPos;
            yield return null;
        }

        transform.position = checkpointPos;

        Time.timeScale = 1f;
        isFollowing = true;
        EventManager.OnPlayerDeath?.Invoke();
        /*for (int i = 0; i < 1; i++)
            yield return new WaitForEndOfFrame();*/
    }

    private void FixedUpdate()
    {
        if (isFollowing)
        {
            Vector3 subjectPos = subject.transform.position;
            transform.position = new Vector3(subjectPos.x, subjectPos.y, -10f);
        }
    }
}
