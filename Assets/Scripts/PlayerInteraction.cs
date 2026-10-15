using System.Drawing;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private GameScore gameScore;
    private PlayerInventory playerInventory;

    private void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit: " + collision.gameObject.name);
        }
        // else if(collision.gameObject.CompareTag("Enemy"))
        // {
        //     EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        //     if(enemyHealth != null)
        //     {
        //         enemyHealth.takeDamage(attackDamage);
        //     }
        // }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Coin"))
        {
            return;
        }

        if(playerInventory == null || gameScore == null)
        {
            return;
        }
        string coinName = collision.gameObject.name.Trim();

        var points = coinName switch
        {
            "BronzeCoin" => 1,
            "SilverCoin" => 5,
            "GoldCoin" => 10,
            _ => 1,
        };
        
        playerInventory.AddCoin(1);
        gameScore.AddScore(points);

        Destroy(collision.gameObject);
    }
}
