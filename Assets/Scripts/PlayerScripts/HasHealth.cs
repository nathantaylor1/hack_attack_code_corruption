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
    public float health_display_time = 3.0f;
    public SpriteMask sm;
    protected bool isInvincible = false;
    bool isFighting = false;
    protected CodeModule module;
    IEnumerator inCombat;
    protected SpriteRenderer sr;
    protected float maxHealth;

    public Transform codeBlockToDrop;
    public Vector2 initialPosition;

    protected virtual void Awake()
    {
        module = GetComponent<CodeModule>();
        sr = GetComponent<SpriteRenderer>();
        maxHealth = health;
        Revive();
        if (TryGetComponent<EnemyID>(out EnemyID eid)) {
            eid.rewind.AddListener(GoBack);
        }
    }

    public void Revive() {
        health = maxHealth;
        SetHealthVisibility(false);
        updateHealthDisplay();
    }

    public void Damage(float damage_amount)
    {
        if (isInvincible) return;

        health -= damage_amount;

        if (gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            if(inCombat != null) StopCoroutine(inCombat);
            inCombat = DisplayHealthBar();
            StartCoroutine(inCombat);
        }

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

    void GoBack() {
        health = maxHealth;
        updateHealthDisplay();
        isInvincible = false;
    }

    protected virtual void Death()
    {
        if (codeBlockToDrop != null)
            codeBlockToDrop.position = transform.position;
        if (TryGetComponent<CodeModule>(out CodeModule cm)) {
            cm.died.Invoke();
        } else {
            gameObject.SetActive(false);
        }
        if (TryGetComponent<EnemyID>(out EnemyID eid)) {
            eid.Add();
        }
    }

    protected virtual void updateHealthDisplay()
    {
        float hp = (health <= 0) ? 0 : health;
        float percent = hp / maxHealth;
        float removed = 1f - percent;
        // x scale should be percent
        sm.transform.localScale = new Vector3(percent, sm.transform.localScale.y, 1);
        sm.transform.localPosition = new Vector3(-removed / 2f, 0, 0);
    }

    IEnumerator DisplayHealthBar()
    {
        isFighting = true;
        SetHealthVisibility(isFighting);
        yield return new WaitForSeconds(health_display_time);
        isFighting = false;
        SetHealthVisibility(isFighting);
    }

    protected void SetHealthVisibility(bool _switch)
    {
        Transform bar = transform.Find("HealthBar");
        if (bar == null) {
            return;
        }
        foreach (Transform child in bar)
        {
            SpriteRenderer temp_sr = child.GetComponent<SpriteRenderer>();
            if(temp_sr != null)
            {
                temp_sr.enabled = _switch;
            }
        }
    }
}
