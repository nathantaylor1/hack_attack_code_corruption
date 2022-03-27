using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private float volume = 0.6f;
    private List<string> soundsPlaying = new List<string>();

    public void SetVolume(float input)
    {
        volume = input;
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (soundsPlaying.Contains(clip.name)) return;
        soundsPlaying.Add(clip.name);

        pos.z = 0;
        AudioSource.PlayClipAtPoint(clip, pos, volume);

        StartCoroutine(WaitToRemoveClip(clip.name));
    }

    private IEnumerator WaitToRemoveClip(string name)
    {
        yield return new WaitForSeconds(0.1f);
        soundsPlaying.Remove(name);
        yield return null;
    }
}
