using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasHealth : MonoBehaviour
{
    public float health = 5;
    public TextMeshProUGUI health_display;

    bool dead = false;
    public bool canInvincible = false;
    bool isInvincible = false;
    
    public bool IsDead()
    {
        return dead;
    }

    private void Start() {
        update_health_display();
    }

    private IEnumerator Invincible() {
        isInvincible = true;
        yield return new WaitForSeconds(.2f);
        isInvincible = false;
    }

    public void Damage(float damage_amount)
    {
        if (isInvincible) {
            return;
        }

        health -= damage_amount;
        if(health <= 0)
        {
            Death();
        } else if (canInvincible) {
            StartCoroutine(Invincible());
        }
        update_health_display();
    }

    public void Heal(float heal_amount)
    {
        health += heal_amount;
        update_health_display();
    }

    void Death()
    {
        if (gameObject.CompareTag("Player"))
        {
            GameManager.instance.ResetLevel();
        } else {
            dead = true;
            gameObject.SetActive(false);
        }
    }

    void update_health_display()
    {
        if (gameObject.CompareTag("Player"))
        {
            float hp = health;
            if (health < 0)
            {
                hp = 0;
            }
            health_display.text = "Health: " + hp.ToString();
        }
    }
}
