using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Player Inventory", order = 1)]
public class PlayerInventorySO : ScriptableObject
{
    /// <summary>
    /// Only use to store inventory data during runtime. Not to be used for saving/loading directly.
    /// </summary>
    /// 
    [System.Serializable]
    public class InventoryItem
    {
        public string itemID;
        public int quantity = 0;
        public bool isEquipped;
    }

    public List<InventoryItem> items = new List<InventoryItem>();
    public int Gold { get; set; } // Total gold the player has
    public int PassTickets { get; set; } // Total pass tickets the player has

    // Add item to inventory (or increase quantity if exists)
    public void AddItem(string itemID, int quantity = 1)
    {
        var existingItem = items.Find(i => i.itemID == itemID);
        if (existingItem != null)
        {
            existingItem.quantity += quantity;
        }
        else
        {
            items.Add(new InventoryItem()
            {
                itemID = itemID,
                quantity = quantity,
                isEquipped = false
            });
        }
    }

    // Remove item from inventory (or decrease quantity)
    public bool RemoveItem(string itemID)
    {
        var item = items.Find(i => i.itemID == itemID);
        if (item == null) return false;
        else
        {
            if (item.quantity <= 0)
            {
                if (item.isEquipped) UnequipItem(itemID);
                items.Remove(item);
            }
            return true;
        }
        return false;
    }

    // Equip an item (also unequips others of same type)
    public bool EquipItem(string itemID, ShopItemType itemType)
    {
        var itemToEquip = items.Find(i => i.itemID == itemID);
        if (itemToEquip == null) return false;

        // First unequip any items of same type
        foreach (var item in items)
        {
            if (item.isEquipped && GetItemType(item.itemID) == itemType)
            {
                item.isEquipped = false;
            }
        }

        itemToEquip.isEquipped = true;
        return true;
    }

    // Unequip an item
    public void UnequipItem(string itemID)
    {
        var item = items.Find(i => i.itemID == itemID);
        if (item != null) item.isEquipped = false;
    }

    // Get quantity of an item
    public int GetItemQuantity(string itemID)
    {
        var item = items.Find(i => i.itemID == itemID);
        return item != null ? item.quantity : 0;
    }

    // Check if item is equipped
    public bool IsItemEquipped(string itemID)
    {
        var item = items.Find(i => i.itemID == itemID);
        return item != null && item.isEquipped;
    }

    public bool ConsumeItem(string itemID, int quantity = 1)
    {
        var item = items.Find(i => i.itemID == itemID);
        if (item == null) return false;
        else if (item != null && item.quantity > 0)
        {
            item.quantity-= quantity;
            if (item.quantity <= 0)
            {
                if (item.isEquipped) UnequipItem(itemID);
                items.Remove(item);
            }
        }
        else
        {
            Debug.LogWarning("Attempted to consume an item that does not exist or has no quantity: " + itemID);
        }
        return true;
    }

    private ShopItemType GetItemType(string itemID)
    {
        var item = items.Find(i => i.itemID == itemID);
        if (item == null) return ShopItemType.Chacter; // Default type if not found
        Debug.Log("Item ID not found in Inventory: " + itemID);
        return ShopItemType.NoType;
    }

    public void SetGoldValue(int value)
    {
        Gold = value;
    }

    public void GainGold(int amount)
    {
        Gold += amount;
    }

    public void PayGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
        }
        else
        {
            Debug.LogWarning("Not enough gold to pay: " + amount);
        }
    }

    public void SetPassTickets(int value)
    {
        PassTickets = value;
    }

    public void GainPassTickets(int amount)
    {
        PassTickets += amount;
    }

    public void UsePassTicket(int amount = 1)
    {
        if (PassTickets >= amount)
        {
            PassTickets -= amount;
        }
        else
        {
            Debug.LogWarning("Not enough pass tickets to pay: " + amount);
        }
    }
}