using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public const int maxHealth = 100;
    public Text healthText;

    public int currentHealth = maxHealth;
    private float lastHit;
    private float lastHeal;

    private void Awake()
    {
        if(healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    private void Update()
    {
        if(Time.time - lastHit >= 10)
        {
            if (Time.time - lastHeal >= 1 && currentHealth < maxHealth)
            {
                currentHealth++;
                lastHeal = Time.time;
            }
            healthText.text = currentHealth.ToString();
        }
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        
        if(healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
        UpdateColor();

        lastHit = Time.time;
    }

    private void UpdateColor()
    {
        if (currentHealth < maxHealth * 1f / 3f)
        {
            healthText.color = Color.red;
        }
        else if (currentHealth <= maxHealth * 2f / 3f)
        {
            healthText.color = Color.yellow;
        }
        else if (currentHealth >= maxHealth * 2f / 3f)
        {
            healthText.color = Color.green;
        }
    }

    void Die()
    {
        //TODO: do something else for players. Would probably want to play death anim here
        Destroy(gameObject);
    }

}
