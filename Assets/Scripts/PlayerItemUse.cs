using UnityEngine;

public class PlayerItemUse : MonoBehaviour
{
    private PlayerInventory inventory;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (inventory != null && playerHealth != null)
            {
                if(playerHealth.LoseHealth() > 50 && inventory.FindItem(4) != null 
                    || inventory.FindItem(5) == null)
                    inventory.UseLargeHealthPotion(playerHealth);
                else
                    inventory.UseHealthPotion(playerHealth);
            }
        }
    }
}
