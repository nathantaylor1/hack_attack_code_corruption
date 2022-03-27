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
    private float maxHealth;

    private void Awake()
    {
        module = GetComponent<CodeModule>();
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
        float hp = (health <= 0) ? 0 : health;
        Debug.Log(hp);
        Debug.Log(maxHealth);
        Debug.Log(gameObject.name);
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
