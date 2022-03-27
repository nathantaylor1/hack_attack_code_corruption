using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasHealth : MonoBehaviour
{
    public float health = 5;
    public TextMeshProUGUI health_display;
    bool isInvincible = false;
    private CodeModule module;

    private void Start()
    {
        module = GetComponent<CodeModule>();
        updateHealthDisplay();
    }

    public void Damage(float damage_amount)
    {
        if (isInvincible) return;

        health -= damage_amount;
        if (module.damageSound != null && AudioManager.instance != null)
            AudioManager.instance.PlaySound(module.damageSound, module.transform.position);
        
        if(health <= 0) Death();
        
        updateHealthDisplay();
    }

    public void Heal(float heal_amount)
    {
        if (module.healSound != null && AudioManager.instance != null)
            AudioManager.instance.PlaySound(module.healSound, module.transform.position);
        health += heal_amount;
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
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            float hp = (health <= 0) ? 0 : health;
            health_display.text = "Health: " + hp;
        }
    }
}
