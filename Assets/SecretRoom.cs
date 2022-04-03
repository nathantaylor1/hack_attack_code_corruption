using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretRoom : MonoBehaviour
{
    public float fadeTime = 0.2f;
    private bool isFaded = false;
    private Tilemap tm;
    private TilemapRenderer tr;

    private void Awake()
    {
        tm = GetComponent<Tilemap>();
        tr = GetComponent<TilemapRenderer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isFaded || !other.CompareTag("Player")) return;
        isFaded = true;
        StartCoroutine(CO_ChangeColorOverTime(Color.white, new Color(0, 0, 0, 0), fadeTime));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isFaded || !other.CompareTag("Player")) return;
        isFaded = false;
        StartCoroutine(CO_ChangeColorOverTime(new Color(0, 0, 0, 0),Color.white, fadeTime));
    }
    
    private IEnumerator CO_ChangeColorOverTime(Color start, Color end, float duration)
    {
        for (float t=0f; t < duration; t+=Time.deltaTime)
        {
            float normalizedTime = t / duration;
            tm.color = Color.Lerp(start, end, normalizedTime);
            yield return null; // wait for next frame before continuing loop
        }

        tm.color = end;

    }
}
