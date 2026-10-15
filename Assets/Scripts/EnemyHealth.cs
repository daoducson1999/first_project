using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int scoreReward = 20;
    [SerializeField]
    private GameScore gameScore;
    [SerializeField]
    private int maxHealth = 50;
    [SerializeField]
    private float KnockbackForce = 5f;
    private int currentHealth;
    private EnemySpawner enemySpawner;
    private Rigidbody2D rb;
    [SerializeField]
    private float knockbackDuration = 0.2f;
    private EnemyMovement enemyMovement;

    [System.Obsolete]
    private void Awake()
    {
        currentHealth = maxHealth;
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void takeDamage(int damage, Vector2 attackerPosition)
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
        ApplyKnockback(attackerPosition);
    }

    private void ApplyKnockback(Vector2 attackerPosition)
    {
        if(rb == null)
        {
            return;
        }

        Vector2 direction = (rb.position - attackerPosition).normalized;

        if(enemyMovement != null)
        {
            enemyMovement.ApplyKnockbackDelay(knockbackDuration);
        }

        rb.linearVelocity = Vector2.zero;
        
        rb.AddForce(
            direction * KnockbackForce,
            ForceMode2D.Impulse
        );
    }
    // public void Die()
    // {
    //     Destroy(gameObject);
    //     enemySpawner.SpawnEnemy();
    // }

    private void Die()
    {
        if(gameScore != null)
        {
            gameScore.AddScore(scoreReward);
        }

        if(enemySpawner != null)
        {
            enemySpawner.SpawnEnemy();
        }

        Destroy(gameObject);
    }
}
