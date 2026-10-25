using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyAI))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private EnemyAI enemyAI;
    //private Transform playerTransform;
    private float knockbackEndTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
    }

    private void FixedUpdate()
    {
        if(Time.time < knockbackEndTime) return;
        if(!enemyAI.IsChasing) return;
        Transform player = enemyAI.Player;
        if(player == null) return;

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        Vector2 targetPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(targetPosition);
    }

    public void ApplyKnockbackDelay(float duration)
    {
        knockbackEndTime = Time.time + duration;
    }
}
