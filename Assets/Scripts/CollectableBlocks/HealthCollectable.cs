using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    public float healAmount = 1.0f;
    private bool _hasCollected = false;
    public AudioClip pickupAudio;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_hasCollected) return;
        if (col.CompareTag("Player"))
        {
            _hasCollected = true;
            HasHealth hh = col.GetComponent<HasHealth>();
            if (hh.IsFullHealth())
            {
                _hasCollected = false;
                return;
            }
            if (AudioManager.instance != null && pickupAudio != null)
                AudioManager.instance.PlaySound(pickupAudio, transform.position);
            hh.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
