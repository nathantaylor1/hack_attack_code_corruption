using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HasHealth : MonoBehaviour
{
    public UnityEvent OnDeath = new UnityEvent();
    public UnityEvent OnRevive = new UnityEvent();

    public float health = 5;
    //public TextMeshProUGUI health_display;
    //public bool deadOnStart = false;
    public Slider healthBarSlider;
    public Image healthBarFill;
    public float health_display_time = 3.0f;
    public SpriteMask sm;
    bool isInvincible = false;
    private CodeModule module;
    IEnumerator inCombat;
    private SpriteRenderer sr;
    [HideInInspector]
    public float maxHealth;
    protected Animator anim;

    public bool IsFullHealth()
    {
        return (health >= maxHealth);
    }

    public Transform codeBlockToDrop;

    private void Awake()
    {
        TryGetComponent<CodeModule>(out module);
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (health > 0) maxHealth = health;
        //Debug.Log(gameObject.name + " has " + health + " starting health and " + maxHealth + " max health");
        SetHealthVisibility(false);
        if(gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EventManager.OnCheckpointSave.AddListener(RestoreFullHealth);
            //EventManager.OnPlayerDeath.AddListener(RestoreFullHealth);
        }
        updateHealthDisplay();
        /*if (deadOnStart)
        {
            Damage(maxHealth);
        }*/
    }

    public void RestoreFullHealth(int _)
    {
        //Debug.Log("Health restored");
        Heal(maxHealth);
    }

    public void Damage(float damage_amount)
    {
        //Debug.Log("player takes damage");
        if (isInvincible || health < 0) return;

        health -= damage_amount;
        if (health < 0) health = 0;

        if (gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            if(inCombat != null) StopCoroutine(inCombat);
            inCombat = DisplayHealthBar();
            StartCoroutine(inCombat);
        }

        if (module != null && module.damageSound != null && AudioManager.instance != null)
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
            yield return new WaitForFixedUpdate();
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
        if (health > maxHealth) health = maxHealth;
        updateHealthDisplay();
    }

    public void Revive()
    {
        health = maxHealth;
        updateHealthDisplay();
        anim.SetTrigger("Idle");
        OnRevive?.Invoke();
    }

    protected virtual void Death()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Player")) {
            //GameManager.instance.ResetLevel();
            health = maxHealth;
            EventManager.OnPlayerDeath?.Invoke();
        }
        else
        {
            if (codeBlockToDrop != null)
                codeBlockToDrop.position = transform.position;
            //gameObject.SetActive(false);
            /*if (TryGetComponent(out CodeModule cm))
            {
                Destroy(cm.editor.window);
                Destroy(cm.editor.button);
            }
            Destroy(gameObject);*/
            //Debug.Log(gameObject.name + " got into death function");
            anim.SetTrigger("Death");
            OnDeath?.Invoke();
        }
    }

    void updateHealthDisplay()
    {
        float hp = (health <= 0) ? 0 : health;
        //Debug.Log("maxHealth: " + maxHealth);
        float percent = hp / maxHealth;
        //Debug.Log("percent: " + percent);
        float removed = 1f - percent;
        //Debug.Log("removed: " + removed);
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //health_display.text = "Health: " + hp;
            healthBarSlider.value = percent;

            Color fillColor = Color.Lerp(Color.red, Color.green, percent);
            healthBarFill.color = fillColor;
        } else {
            // x scale should be percent
            if (sm != null)
            {
                sm.transform.localScale = new Vector3(percent, sm.transform.localScale.y, 1);
                sm.transform.localPosition = new Vector3(-removed / 2f, 0, 0);
            }
        }
    }

    IEnumerator DisplayHealthBar()
    {
        SetHealthVisibility(true);
        yield return new WaitForSeconds(health_display_time);
        SetHealthVisibility(false);
    }

    void SetHealthVisibility(bool _switch)
    {
        if (gameObject.tag == "Player") return;
        Transform bar = transform.Find("HealthBar");
        foreach (Transform child in bar)
        {
            SpriteRenderer temp_sr = child.GetComponent<SpriteRenderer>();
            if (temp_sr != null)
            {
                temp_sr.enabled = _switch;
            }
        }
    }
}