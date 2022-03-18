using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasHealth : MonoBehaviour
{
    public float health = 30;
    public TextMeshProUGUI health_display;

    bool dead = false;
    
    public bool IsDead()
    {
        return dead;
    }

    public void Damage(float damage_amount)
    {
        health -= damage_amount;
        if(health <= 0)
        {
            Death();
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
        }
        dead = true;
        gameObject.SetActive(false);
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
            health_display.text = hp.ToString();
        }
    }
}
