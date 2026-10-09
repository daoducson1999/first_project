using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 50;

    private int currentHealth;
    private EnemySpawner enemySpawner;

    [System.Obsolete]

    private void Awake()
    {
        currentHealth = maxHealth;
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
    }

    public void takeDamage(int damage)
    {
        if(damage <= 0)
        {
            return;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        Debug.Log("Enemy Health: " + currentHealth);

        if(currentHealth == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        enemySpawner.SpawnEnemy();
    }
}
