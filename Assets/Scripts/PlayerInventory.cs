using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private CoinCounterUI coinCounterUI;

    [SerializeField]
    private InventoryUI inventoryUI;
    private int coinCount;
    const int maxSlot = 5;

    public IReadOnlyList<Item> Items
    {
        get { return items; }
    }
    private List<Item> items = new();

    private void RefreshUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.Refresh(items);
        }
    }

    public Item FindItem(int id)
    {
        foreach (Item item in items)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }


    public bool AddItem(Item newItem)
    {   
        if(newItem == null) return false;
        Item existingItem = FindItem(newItem.Id);
        int NoOfItem = items.Count;
        if (existingItem != null)
        {
            existingItem.Quantity += newItem.Quantity;
            SaveInventory();
            RefreshUI();
            return true;
        }
        else if (NoOfItem < maxSlot)
        {
            items.Add(newItem);
            SaveInventory();
            RefreshUI();
            return true;
        }

        return false;
    }

    public bool RemoveItem(int id, int amount)
    {
        if (amount <= 0) return false;

        Item item = FindItem(id);
        if (item == null) return false;
        if (item.Quantity < amount) return false;

        item.Quantity -= amount;
        if (item.Quantity == 0)
        {
            items.Remove(item);
        }
        SaveInventory();
        RefreshUI();
        return true;
    }

    public bool HasItem(int id, int amount)
    {
        if (amount <= 0) return false;

        Item item = FindItem(id);
        if (item == null) return false;
        if (item.Quantity < amount)
            return false;
        else
            return true;
    }

    public void UseHealthPotion(PlayerHealth playerHealth)
    {
        const int healthPotionId = 5;
        bool removed = RemoveItem(healthPotionId, 1);
        if (!removed) return;

        playerHealth.Heal(25);
    }

    public void UseLargeHealthPotion(PlayerHealth playerHealth)
    {
        const int healthPotionId = 4;
        bool removed = RemoveItem(healthPotionId, 1);
        if (!removed) return;

        playerHealth.Heal(50);
    }

    public int CoinCount
    {
        get
        {
            return coinCount;
        }
    }

    private void Start()
    {
        LoadInventory();
        if (coinCounterUI != null)
        {
            coinCounterUI.SetCoinCount(coinCount);
        }
        RefreshUI();
    }

    public void AddCoin(int amount)
    {
        if (amount <= 0) return;

        coinCount += amount;
        SaveInventory();
        if (coinCounterUI != null)
        {
            coinCounterUI.SetCoinCount(coinCount);
        }
    }

    private void LoadInventory()
    {
        if (GameSession.Instance == null)
        {
            return;
        }

        coinCount = GameSession.Instance.CoinCount;
        items = GameSession.Instance.LoadItems();
    }

    private void SaveInventory()
    {
        if (GameSession.Instance == null)
        {
            return;
        }

        GameSession.Instance.CoinCount = coinCount;
        GameSession.Instance.SaveItems(items);
    }
}
