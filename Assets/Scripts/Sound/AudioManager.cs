using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;
    private AudioSource musicSrc;
    private Transform mainCamT;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        SceneManager.sceneLoaded += PlayMusic;
    }
    
    private List<string> soundsPlaying = new List<string>();

    public void SetFXVolume(float input)
    {
        AudioManagerStorage.fxVol = input;
    }

    public float GetFXVolume()
    {
        return AudioManagerStorage.fxVol;
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (soundsPlaying.Contains(clip.name)) return;
        soundsPlaying.Add(clip.name);

        pos.z = -10;
        AudioSource.PlayClipAtPoint(clip, pos, AudioManagerStorage.fxVol);

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
        AudioManagerStorage.musicVol = input;
        musicSrc.volume = input;
    }

    public float GetMusicVolume()
    {
        return AudioManagerStorage.musicVol;
    }
    
    public void PlayMusic(Scene scene, LoadSceneMode mode)
    {
        if (Camera.main)
        {
            mainCamT = Camera.main.transform;
            musicSrc = Camera.main.gameObject.AddComponent<AudioSource>();
        }
        musicSrc.clip = (SceneManager.GetActiveScene().name == "MainMenu") ? mainMenuMusic : gameMusic;
        musicSrc.loop = true;
        
        SetMusicVolume(AudioManagerStorage.musicVol);
        SetFXVolume(AudioManagerStorage.fxVol);
        
        musicSrc.Play();
    }

    public void PlayGlobalSound(AudioClip clip)
    {
        Vector3 pos = mainCamT.position;
        pos.z = 0;
        AudioSource.PlayClipAtPoint(clip, pos, AudioManagerStorage.fxVol);
    }
}
