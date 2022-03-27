using UnityEngine;
using UnityEngine.UI;

public class HasHealth : MonoBehaviour
{
    public float health = 5;
    //public TextMeshProUGUI health_display;
    public Slider healthBarSlider;
    public Image healthBarFill;
    bool isInvincible = false;
    private CodeModule module;
    private float maxHealth;

    private void Awake()
    {
        module = GetComponent<CodeModule>();
        updateHealthDisplay();
        maxHealth = health;
    }

    public void Damage(float damage_amount)
    {
        Debug.Log("player takes damage");
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
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            float hp = (health <= 0) ? 0 : health;
            float percent = hp / maxHealth;
            //health_display.text = "Health: " + hp;
            healthBarSlider.value = percent;

            Color fillColor = Color.Lerp(Color.red, Color.green, percent);
            healthBarFill.color = fillColor;
        }
    }
}
