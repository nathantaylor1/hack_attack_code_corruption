using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public static CrosshairController instance = null;
    protected Transform target = null;
    protected SpriteRenderer sr = null;
    protected Color visibleColor = Color.white;
    protected Color invisibleColor = Color.clear;

    protected void Awake()
    {
        instance = this;
        sr = GetComponent<SpriteRenderer>();
        visibleColor = sr.color;
        visibleColor.a = 1f;
        EventManager.OnPlayerDeath.AddListener(Hide);
    }

    protected void FixedUpdate()
    {
        if (target != null)
            transform.position = target.position;
    }

    public void SetTarget(Transform _target)
    {
        sr.color = visibleColor;
        target = _target;
    }

    public void Hide()
    {
        //Debug.Log("Hidden");
        sr.color = invisibleColor;
    }
}
