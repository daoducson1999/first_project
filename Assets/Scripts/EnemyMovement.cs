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
    [SerializeField] private Vector2 pointAOffset = new Vector2(-2f, 0f);
    [SerializeField] private Vector2 pointBOffset = new Vector2(2f, 0f);
    [SerializeField] public float patrolSpeed = 2f;

    private float speed; 
    private Vector2 pointA;
    private Vector2 pointB;
    private bool movingToPointA;
    [SerializeField] private Animator animator;
    private Vector2 direction;
    private Vector2 lastDirection = Vector2.down;
    private void UpdateAnimation()
    {
        if (animator == null) return;

        if (direction.sqrMagnitude >= 0.5f)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                lastDirection = direction.x > 0
                ? Vector2.right
                : Vector2.left;
            }
            else
            {
                lastDirection = direction.y > 0
                ? Vector2.up
                : Vector2.down;
            }
        }

        animator.SetFloat("DirectionX", lastDirection.x);
        animator.SetFloat("DirectionY", lastDirection.y);
        animator.SetFloat("Speed", speed);
    }
    private void Awake()
    {
        pointA = (Vector2)transform.position + pointAOffset;
        pointB = (Vector2)transform.position + pointBOffset;
        movingToPointA = Vector2.Distance(transform.position, pointA)
            <= Vector2.Distance(transform.position, pointB);
        rb = GetComponent<Rigidbody2D>();
        speed = patrolSpeed;
        enemyAI = GetComponent<EnemyAI>();
        if (animator == null)
            animator = GetComponent<Animator>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (Time.time < knockbackEndTime) return;
        if (enemyAI.IsChasing)
        {
            Transform player = enemyAI.Player;
            if (player == null) return;
            speed = moveSpeed;
            direction = ((Vector2)player.position - rb.position).normalized;
            Vector2 targetPosition = rb.position + direction * speed * Time.fixedDeltaTime; 
            rb.MovePosition(targetPosition);
        }
        else if (enemyAI.IsPatrol)
        {
            Vector2 patrolTarget = movingToPointA ? pointA : pointB;
            speed = patrolSpeed;
            direction = (patrolTarget - rb.position).normalized;
            Vector2 targetPosition = rb.position + direction * speed * Time.fixedDeltaTime;

            if (Vector2.Distance(rb.position, patrolTarget) <= 0.05f)
            {
                movingToPointA = !movingToPointA;
            }
            rb.MovePosition(targetPosition);
        }
        UpdateAnimation();
    }

    public void ApplyKnockbackDelay(float duration)
    {
        knockbackEndTime = Time.time + duration;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(
            transform.position,
            Vector3.forward,
            enemyAI.ChaseRange
        );
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(
            transform.position,
            Vector3.forward,
            enemyAI.AttackRange
        );

        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(
                pointA,
                pointB
            );
        }
    }
#endif
}
