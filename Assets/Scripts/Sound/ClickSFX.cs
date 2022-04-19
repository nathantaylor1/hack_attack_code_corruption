using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSFX : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    private AudioSource clickSource;

    private void Start()
    {
        clickSource = Camera.main.gameObject.AddComponent<AudioSource>();
        clickSource.clip = clickSound;
    }

    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickSource.volume = AudioManager.instance.GetFXVolume() * 0.1f;
            clickSource.Play();
        }
    }
}
