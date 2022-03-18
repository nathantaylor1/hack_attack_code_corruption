using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HasHealth : MonoBehaviour
{
    public float health = 30;
    public TextMeshProUGUI health_display;

    bool is_dead = false;
    
    public bool Is_Dead()
    {
        return is_dead;
    }

    public void Damage(float damage_amount)
    {
        health -= damage_amount;
        if(health <= 0)
        {
            Death();
        }
        else
        {
            is_dead = true;
            this.gameObject.SetActive(false);
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
        if (this.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void update_health_display()
    {
        if (this.gameObject.CompareTag("Player"))
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
