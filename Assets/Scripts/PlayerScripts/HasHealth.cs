using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HasHealth : MonoBehaviour
{
    public float health = 5;
    //public TextMeshProUGUI health_display;
    public Slider healthBarSlider;
    public Image healthBarFill;
    public SpriteMask sm;
    bool isInvincible = false;
    private CodeModule module;
    private SpriteRenderer sr;
    private float maxHealth;

    private void Awake()
    {
        module = GetComponent<CodeModule>();
        sr = GetComponent<SpriteRenderer>();
        maxHealth = health;
        updateHealthDisplay();
    }

    public void Damage(float damage_amount)
    {
        Debug.Log("player takes damage");
        if (isInvincible) return;

        health -= damage_amount;
        if (module.damageSound != null && AudioManager.instance != null)
            AudioManager.instance.PlaySound(module.damageSound, module.transform.position);
        
        if(health <= 0) {
            Death();
        } else {
            isInvincible = true;
            StartCoroutine(PlayInvincible());
        }
        
        updateHealthDisplay();
    }
    public IEnumerator PlayInvincible() {
        Color dmg = new Color(.6f, .2f, .2f, .8f);
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForEndOfFrame();
            if (i % 10 == 0 && sr != null) {
                sr.color = dmg;
            } else if (i % 5 == 0 && sr != null) {
                sr.color = Color.white;
            }
        }
        isInvincible = false;
    }

    public void Heal(float heal_amount)
    {
        if (module.healSound != null && AudioManager.instance != null)
            AudioManager.instance.PlaySound(module.healSound, module.transform.position);
        health += heal_amount;
        if (health > maxHealth) maxHealth = health;
        updateHealthDisplay();
    }

    void Death()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Player")) {
            GameManager.instance.ResetLevel();
        } else {
            gameObject.SetActive(false);
        }
    }

    void updateHealthDisplay()
    {
        float hp = (health <= 0) ? 0 : health;
        float percent = hp / maxHealth;
        float removed = 1f - percent;
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //health_display.text = "Health: " + hp;
            healthBarSlider.value = percent;

            Color fillColor = Color.Lerp(Color.red, Color.green, percent);
            healthBarFill.color = fillColor;
        } else {
            // x scale should be percent
            sm.transform.localScale = new Vector3(percent, sm.transform.localScale.y, 1);
            sm.transform.localPosition = new Vector3(-removed / 2f, 0, 0);
        }
    }
}
