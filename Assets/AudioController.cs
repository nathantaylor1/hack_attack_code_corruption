using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;
    AudioSource _audioSource;
    AudioClip _jumpSound, _pickUpSound, _openSound, _stapeSound, _playerHitSound, _enemyHitSound;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        _audioSource = gameObject.AddComponent<AudioSource>();
        _jumpSound = Resources.Load<AudioClip>("Audio/jump2");
        _pickUpSound = Resources.Load<AudioClip>("Audio/pickup0");
        _openSound = Resources.Load<AudioClip>("Audio/open0");
        _stapeSound = Resources.Load<AudioClip>("Audio/shoot1");
        _playerHitSound = Resources.Load<AudioClip>("Audio/playerHit");
        _enemyHitSound = Resources.Load<AudioClip>("Audio/enemyHit");

        _audioSource.volume = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayJump()
    {
        _audioSource.PlayOneShot(_jumpSound);
    }

    public void PlayPickUp()
    {
        _audioSource.PlayOneShot(_pickUpSound);
    }

    public void PlayOpen()
    {
        _audioSource.PlayOneShot(_openSound);
    }

    public void PlayPlayerHit()
    {
        _audioSource.PlayOneShot(_playerHitSound);
    }

    public void PlayEnemyHit()
    {
        _audioSource.PlayOneShot(_enemyHitSound);
    }
}
