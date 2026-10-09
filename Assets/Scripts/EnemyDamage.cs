using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    private void OnColliionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.takeDamage(damage);
            }
        }
    }
}
