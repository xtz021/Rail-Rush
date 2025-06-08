using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[CreateAssetMenu(fileName = "ShopItemsData", menuName = "Scriptable Objects/Shop Items Data", order = 2)]
public class ShopItemsData : ScriptableObject
{
    public List<ShopItem> shopItems;

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
            Debug.LogError("Index out of range: " + index);
            return;
        }
        ShopItem item = shopItems[index];
        if (item.isIAP)
        {
            // Handle IAP logic here
            Debug.Log("Purchasing IAP item: " + item.name);
        }
        else
        {
            // Handle gold purchase here
            if (PlayerInventoryManager.Instance.playerInventorySO.Gold >= item.price)
            {
                //PlayerInventoryManager.Instance.playerInventorySO.Gold -= item.price;
                PlayerInventoryManager.Instance.PayGold(item.price);
                Debug.Log("Purchased item: " + item.name);
                item.isPurchased = true; // Mark the item as purchased
            }
            else
            {
                Debug.LogWarning("Not enough gold to purchase: " + item.name);
            }
        }
    }




    public void EquipItem(int index)
    {
        if (index < 0 || index >= shopItems.Count)
        {
            Debug.LogError("Index out of range: " + index);
            return;
        }
        ShopItem item = shopItems[index];
        if (item.isPurchased)
        {
            // Un-equip previously equipped item of the same type
            foreach (var shopItem in shopItems)
            {
                if (shopItem.isEquipped && shopItem.itemType == item.itemType)
                {
                    shopItem.isEquipped = false;
                    break;
                }
            }
            // Equip this item
            item.isEquipped = true;
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
        if (item.isEquipped)
        {
            item.isEquipped = false;
            Debug.Log("Unequipped item: " + item.name);
        }
        else
        {
            Debug.LogWarning("Item is not equipped: " + item.name);
        }
    }
}
