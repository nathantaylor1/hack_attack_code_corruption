using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHasHealth : HasHealth
{
    protected override void Awake()
    {
        base.Awake();
        CheckpointManager.PlayerKilled.AddListener(Rewind);
    }
    protected override void Death()
    {
        GameManager.instance.GoBackToPreviousCheckpoint();
        Rewind();
    }

    public void Rewind() {
        health = maxHealth;
        isInvincible = false;
        updateHealthDisplay();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = CheckpointManager.playerPos;
        GameManager.instance.reloading = false;
    }

    protected override void updateHealthDisplay()
    {
        float hp = (health <= 0) ? 0 : health;
        float percent = hp / maxHealth;
        //health_display.text = "Health: " + hp;
        healthBarSlider.value = percent;

        Color fillColor = Color.Lerp(Color.red, Color.green, percent);
        healthBarFill.color = fillColor;
    }
}
