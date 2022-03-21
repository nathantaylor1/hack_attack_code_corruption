using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Vector2 spawnPoint;
    private Transform playertf;
    private Rigidbody2D rb2d;
    private void Awake()
    {
        playertf = GameManager.instance.player.transform;
        rb2d = GameManager.instance.player.GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;
        Spawn();
    }

    public void Spawn()
    {
        playertf.position = spawnPoint;
        rb2d.velocity = Vector2.zero;
    }
}
