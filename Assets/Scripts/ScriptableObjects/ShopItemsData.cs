using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[CreateAssetMenu(fileName = "ShopItemsData", menuName = "Scriptable Objects/Shop Items Data", order = 2)]
public class ShopItemsData : ScriptableObject
{
    public List<ShopItem> shopItems;

    [HideInInspector] public const string PASSTICKET_ID = "ID_Pass"; // ID for the pass ticket item

    public int ItemsCount
    {
        get { return shopItems != null ? shopItems.Count : 0; }
    }

    public ShopItem GetItem(int index)
    {
        if (index < 0 || index >= shopItems.Count)
        {
            Debug.LogError("Index out of range: " + index);
            return null;
        }
        return shopItems[index];
    }

    public void PurchaseItem(int index)
    {
        if (index < 0 || index >= shopItems.Count)
        {
            Debug.LogError($"Index {index} out of range: " + shopItems.Count);
            return;
        }
        ShopItem item = shopItems[index];
        if (item.isIAP)
        {
            // Handle IAP here
            Debug.Log("Purchasing IAP item: " + item.name);
            IAPManager.Instance.BuyProduct(item.iapID);
        }
        else
        {
            // Handle gold purchase here
            if (InventoryManager.Instance.inventory.Gold >= item.price)
            {
                InventoryManager.Instance.inventory.PayGold(item.price);
                Debug.Log("Purchased item: " + item.name);
                if(item.itemID == PASSTICKET_ID)        // Check if the item is a pass ticket pack
                {
                    InventoryManager.Instance.inventory.GainPassTickets(item.purchaseAmount); // Add pass tickets to inventory
                }
                else if(item.isConsumable) // If the item is consumable, add it to the inventory
                {
                    InventoryManager.Instance.inventory.AddItem(item.itemID,item.purchaseAmount); // Add item to inventory
                }
                else // If the item is not consumable, just add it to the inventory
                {
                    InventoryManager.Instance.inventory.AddItem(item.itemID); // Add item to inventory
                }
                if(ShopUIManager.Instance.gameObject.activeSelf == true)
                {
                    GameMenuUIController.Instance.PopUpNotice($"{item.name}\nPurchase successful!");
                }
            }
            else
            {
                GameMenuUIController.Instance.PopUpNotice("Not enough gold!");
                Debug.LogWarning("Not enough gold to purchase: " + item.name);
            }
        }
        InventoryManager.Instance.SaveInventory(); // Save inventory after purchase
    }


    public void EquipItem(int index)
    {
        if (index < 0 || index >= shopItems.Count)
        {
            Debug.LogError("Index out of range: " + index);
            return;
        }
        ShopItem item = shopItems[index];
        if (InventoryManager.Instance.IsPurchased(item.itemID))
        {
            // Equip this item  - this will also unequip any other item of the same type
            InventoryManager.Instance.EquipItem(item.itemID, item.itemType);
            Debug.Log("Equipped item: " + item.name);
        }
        else
        {
            Debug.LogWarning("Item not purchased: " + item.name);
        }
    }

    public void UnequipItem(int index)
    {
        if (index < 0 || index >= shopItems.Count)
        {
            Debug.LogError("Index out of range: " + index);
            return;
        }
        ShopItem item = shopItems[index];
        if (InventoryManager.Instance.IsEquipped(item.itemID))
        {
            InventoryManager.Instance.UnequipItem(item.itemID);
            Debug.Log("Unequipped item: " + item.name);
        }
        else
        {
            Debug.LogWarning("Item is not equipped: " + item.name);
        }
    }

    public int GetItemIndex(ShopItem item)
    {
        return shopItems.IndexOf(item);
    }

}
