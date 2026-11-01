using NUnit.Framework.Internal.Builders;
using Unity.Mathematics;
using UnityEngine;
public enum EnemyState
{
    Idle, Chase, Attack, Patrol
}
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float chaseRange = 6f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private Vector2 pointAOffset = new Vector2(-2f, 0f);
    [SerializeField] private Vector2 pointBOffset = new Vector2(2f, 0f);
    [SerializeField] public float patrolSpeed = 2f;
    private EnemyState currentState = EnemyState.Patrol;
    public Vector2 pointA;
    public Vector2 pointB;
    public bool movingToPointA;
    public bool IsChasing => currentState == EnemyState.Chase;
    public bool IsPatrol => currentState == EnemyState.Patrol;
    private EnemyCombat enemyCombat;
    public Transform Player => player;
    public float AttackRange => attackRange;
    public float ChaseRange => chaseRange;

    private void Awake()
    {
        enemyCombat = GetComponent<EnemyCombat>();
        

        if(player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if(playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    private void HandleIdle()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if(distance <= chaseRange)
        {
            currentState = EnemyState.Chase;
        }
    }

    private void HandlePatrol()
    {   
        // Vector2 patrolTarget = movingToPointA ? pointA : pointB;
     
        // Vector2 nextPosition = Vector2.MoveTowards(
        //     transform.position,
        //     patrolTarget,
        //     patrolSpeed * Time.deltaTime
        // );

        // transform.position = new Vector3(nextPosition.x, nextPosition.y, transform.position.z);

        // if (Vector2.Distance(transform.position, patrolTarget) <= 0.05f)
        // {
        //     movingToPointA = !movingToPointA;
        // }
    }

    private void HandleChase()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if(distance > chaseRange)
        {
            currentState = EnemyState.Idle;
            return;
        }
        if(distance <= attackRange)
        {
            currentState = EnemyState.Attack;
            return;
        }

        //MoveTowardPlayer();
    }


    private void HandleAttack()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if(distance > attackRange)
        {
            currentState = EnemyState.Chase;
            return;
        }
        enemyCombat.TryAttack(player);
    }

    private void ChangeState(EnemyState newState)
    {
        if(currentState == newState) return;
        currentState = newState;
        Debug.Log(gameObject.name + " State: " + currentState);
    }

    private void UpdateState()
    {
        if (player == null)
        {
            ChangeState(EnemyState.Patrol);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        if(distance > chaseRange) 
            ChangeState(EnemyState.Patrol);
        else if(distance <= attackRange)
            ChangeState(EnemyState.Attack);
        else
            ChangeState(EnemyState.Chase);
    }

    private void Update()
    {
        UpdateState();
        switch(currentState)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;
            case EnemyState.Chase:
                HandleChase();
                break;
            case EnemyState.Attack:
                HandleAttack();
                break;
            case EnemyState.Patrol:
                HandlePatrol();
                break;
        }
    }

}
