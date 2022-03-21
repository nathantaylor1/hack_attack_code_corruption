using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuDoor : MonoBehaviour
{
    public GameObject hiderImage, hiderCamera;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            hiderCamera.SetActive(true);
            hiderImage.SetActive(true);
            Destroy(GameManager.instance.player);
            Destroy(GameManager.instance.UI);
            Destroy(GameManager.instance.gameObject);
            SceneManager.LoadScene(1);
        }
    }
}
