using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        //PhotonNetwork.Destroy(gameObject);
        Destroy(gameObject);
        PhotonView pv = GetComponent<PhotonView>();
        if (pv != null && pv.isMine)
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
        }
    }
}
