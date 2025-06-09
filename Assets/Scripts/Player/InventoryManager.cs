using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public PlayerInventorySO inventory;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadInventory();
            AddStartingItem();
        }
    }

    public void SaveInventory()
    {
        string json = JsonUtility.ToJson(inventory);
        string path = Path.Combine(Application.persistentDataPath, "inventory.json");
        File.WriteAllText(path, json);
    }

    public void LoadInventory()
    {
        string path = Path.Combine(Application.persistentDataPath, "inventory.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, inventory);
        }
    }

    private void AddStartingItem()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory is not initialized.");
            return;
        }
        // Check if the item already exists
        if (!IsPurchased("ID_Char1"))
        {
            // Add a starting item if it doesn't exist
            inventory.AddItem("ID_Char1"); // Add the starting item (char1)
            inventory.items.Find(i => i.itemID == "ID_Char1").isEquipped = true; // Equip the starting item
            SaveInventory();
        }   
    }

    // Called when purchasing an item
    public void OnItemPurchased(string itemID, bool isConsumable)
    {
        inventory.AddItem(itemID, isConsumable ? 1 : 1); // Add 1 quantity
        SaveInventory();
    }

    // Called when using a consumable
    public void UseConsumable(string itemID, int amount = 1)
    {
        if (inventory.ConsumeItem(itemID))
        {
            // Apply item effect here
            SaveInventory();
        }
    }

    // Called when equipping an item
    public void EquipItem(string itemID, ShopItemType itemType)
    {
        if (inventory.EquipItem(itemID, itemType))
        {
            SaveInventory();
        }
    }

    // Called when unequipping an item
    public void UnequipItem(string itemID)
    {
        inventory.UnequipItem(itemID);
        SaveInventory();
    }

    public bool IsPurchased(string itemID)
    {
        if (inventory != null)
        {
            foreach (var item in inventory.items)
            {
                if (item.itemID == itemID)
                {
                    return true; // Item is already purchased
                }
            }
            Debug.LogWarning("Item not found in inventory: " + itemID);
            return false; // Item not found in inventory
        }
        Debug.LogError("Inventory is not initialized.");
        return false; // Item not found in inventory
    }

    public bool IsEquipped(string itemID)
    {
        if (inventory != null)
        {
            foreach (var item in inventory.items)
            {
                if (item.itemID == itemID && item.isEquipped)
                {
                    return true; // Item is equipped
                }
            }
            Debug.LogWarning("Item not found or not equipped: " + itemID);
            return false; // Item not found or not equipped
        }
        Debug.LogError("Inventory is not initialized.");
        return false; // Item not found or not equipped
    }
}