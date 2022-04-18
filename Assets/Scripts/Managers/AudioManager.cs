using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip music;
    private AudioSource musicSrc;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        if (Camera.main != null) musicSrc = Camera.main.gameObject.AddComponent<AudioSource>();
        if (musicSrc != null) PlayMusic();
    }

    private float fxVol = 0.6f;
    private float musicVolume = 0.07f;
    private List<string> soundsPlaying = new List<string>();

    public void SetFXVolume(float input)
    {
        fxVol = input;
    }

    public float GetFXVolume()
    {
        return fxVol;
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (soundsPlaying.Contains(clip.name)) return;
        soundsPlaying.Add(clip.name);

        pos.z = 0;
        AudioSource.PlayClipAtPoint(clip, pos, fxVol);

        StartCoroutine(WaitToRemoveClip(clip.name));
    }

    private IEnumerator WaitToRemoveClip(string name)
    {
        yield return new WaitForSeconds(0.1f);
        soundsPlaying.Remove(name);
        yield return null;
    }

    public void SetMusicVolume(float input)
    {
        musicVolume = input;
        musicSrc.volume = input;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }
    
    public void PlayMusic()
    {
        musicSrc.clip = music;
        musicSrc.loop = true;
        musicSrc.volume = musicVolume;
        musicSrc.Play();
    }
}
