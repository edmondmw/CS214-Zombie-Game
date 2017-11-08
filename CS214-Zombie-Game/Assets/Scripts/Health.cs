using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public const int maxHealth = 100;
    public Text healthText;

    public int currentHealth = maxHealth;

    private void Awake()
    {
        if(healthText != null)
        {
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
    }

    void Die()
    {
        //TODO: do something else for players. Would probably want to play death anim here
        Destroy(gameObject);
    }
}
