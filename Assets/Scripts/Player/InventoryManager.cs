using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public PlayerInventorySO inventory;

    public int NuggetBonusMultiplier = 1; // Multiplier for nugget bonus

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
            //ResetInventoryData();
        }
    }

    private void Start()
    {
        CheckForIAPPurchases(); // Check for IAP purchases and add them to inventory
        CheckBonusItems();
    }

    private void OnDisable()
    {
        SaveInventory(); // Save inventory when the game is closed or this object is destroyed
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
        else
        {
            Debug.LogWarning("Inventory file not found, initializing new inventory.");
            inventory = ScriptableObject.CreateInstance<PlayerInventorySO>();
            inventory.items = new List<PlayerInventorySO.InventoryItem>();
            inventory.Gold = 0;
            inventory.PassTickets = 0;
            AddStartingItem(); // Add starting item if inventory is empty
        }
    }

    public void ResetInventoryData()
    {
        string path = Path.Combine(Application.persistentDataPath, "inventory.json");
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.LogWarning("Inventory file not found, initializing new inventory.");
        }
        inventory = ScriptableObject.CreateInstance<PlayerInventorySO>();
        inventory.items = new List<PlayerInventorySO.InventoryItem>();
        inventory.Gold = 0;
        inventory.PassTickets = 0;
        AddStartingItem();
        SaveInventory();
    }

    private void CheckForIAPPurchases()
    {
        ShopItemsData heroesShopData = ItemIDsContainers.Instance.shopHeroesData;
        ShopItemsData cartStuffShopData = ItemIDsContainers.Instance.shopCartStuffsData;
        ShopItemsData extraShopData = ItemIDsContainers.Instance.shopExtrasData;
        foreach (var item in heroesShopData.shopItems)
        {
            if (item.isIAP && IAPManager.Instance.IsPurchased(item.iapID) && !IsPurchased(item.itemID))
            {
                if(!item.isConsumable)          // Only load a non-consumable item since cannot buy it again
                {
                    inventory.AddItem(item.itemID, 1, false);
                }
            }
        }
        foreach (var item in cartStuffShopData.shopItems)
        {
            if (item.isIAP && IAPManager.Instance.IsPurchased(item.iapID) && !IsPurchased(item.itemID))
            {
                if(!item.isConsumable)          // Only load a non-consumable item since cannot buy it again
                {
                    inventory.AddItem(item.itemID, 1, false);
                }
            }
        }
        foreach (var item in extraShopData.shopItems)
        {
            if (item.isIAP && IAPManager.Instance.IsPurchased(item.iapID) && !IsPurchased(item.itemID))
            {
                if(!item.isConsumable)          // Only load a non-consumable item since cannot buy it again
                {
                    inventory.AddItem(item.itemID, 1, false);
                }
            }
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
        CheckBonusItems(); // Check for bonus items
    }

    // Called when using a consumable
    public bool UseConsumable(string itemID, int amount = 1)
    {
        if (inventory.ConsumeItem(itemID))
        {
            // Apply item effect here
            SaveInventory();
            return true; // Consumable used successfully
        }
        else
        {
            Debug.LogWarning("Consumable not found or insufficient quantity: " + itemID);
            return false; // Consumable not found or insufficient quantity
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

    public void CheckBonusItems()
    {
        CheckNuggetBonusX2(); // Check if Nugget Bonus X2 is purchased
    }

    private void CheckNuggetBonusX2()
    {
        if (IsPurchased("ID_NuggetBonusX2"))
        {
            NuggetBonusMultiplier = 2; // Set multiplier to 2 if the item is purchased
        }
        else
        {
            NuggetBonusMultiplier = 1; // Default multiplier
        }
    }

}