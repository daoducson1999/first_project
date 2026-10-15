using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private HealthBarUI healthBarUI;
    [SerializeField]
    private GameManager gameManager;
    private int currentHealth;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        if(healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
        }
    }

    public void takeDamage(int damage)
    {
        if(damage <= 0)
        {
            return;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        //Debug.Log("Player Health: " + currentHealth);
        if(healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }

        if(currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Die!");
        if(gameManager != null)
        {
            gameManager.GameOver();
        }
        gameObject.SetActive(false);
    }

}
