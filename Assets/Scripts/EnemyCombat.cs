using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private float attackCooldown = 1f;
    [SerializeField] private Animator animator;
    private float nextAttackTime;
    public void TryAttack(Transform target)
    {
        if(target == null) return;
        if(Time.time < nextAttackTime) return;
        if(!target.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth)) return;
        if (animator != null)
        {
            animator.SetBool("isAttacking", true);
            animator.SetTrigger("ATK");
        }
        playerHealth.takeDamage(damage);
        nextAttackTime = Time.time + attackCooldown;
    }

    public void EndAttackAnimation()
    {
        if(animator != null)
        {
            animator.SetBool("isAttacking", false);
        }

    }
}
