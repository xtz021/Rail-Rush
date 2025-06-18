using System.Collections;
using System.IO;
using UnityEngine;
using static PlayerInventorySO;

public class ShopItemsListManager : MonoBehaviour
{
    [Header("Shop Item Data")]
    [SerializeField] ShopItemsData itemsData; // This will hold the data for all items in the shop
    [SerializeField] GameObject shopItemPrefab; // Prefab for each shop item
    [SerializeField] Transform shopItemsContainer; // Parent container for shop items

    private void Start()
    {
        if (itemsData != null && shopItemsContainer != null && shopItemPrefab != null)
        {
            GenerateShopItems();
        }
        else
        {
            Debug.LogError("ShopItemsData, shopItemsContainer, or shopItemPrefab is not set.");
        }
    }

    private void GenerateShopItems()
    {
        if (itemsData == null || shopItemsContainer == null || shopItemPrefab == null)
        {
            Debug.LogError("ShopItemsData, shopItemsContainer, or shopItemPrefab is not set.");
            return;
        }
        foreach (Transform child in shopItemsContainer)
        {
            Destroy(child.gameObject); // Clear existing items
        }
        for (int i = 0; i < itemsData.ItemsCount; i++)
        {
            ShopItem item = itemsData.GetItem(i);
            if (item != null)
            {
                int index = i; // Capture the current index for the lambda expression
                GameObject itemGO = Instantiate(shopItemPrefab, shopItemsContainer);
                ShopItemUIHandler itemUIHandler = itemGO.GetComponent<ShopItemUIHandler>();
                if (itemUIHandler != null)
                {
                    itemUIHandler.SetIcon(item.iconImage);
                    itemUIHandler.SetName(item.name);
                    itemUIHandler.SetDescription(item.description);
                    itemUIHandler.SetDescriptionImage(item.descriptionImage);
                    itemUIHandler.SetPrice(item.price, item.isIAP);
                    itemUIHandler.SetBuyButtonEvent(() => itemsData.PurchaseItem(index));
                    itemUIHandler.AddBuyButtonEvent(RefreshShopItems);
                    itemUIHandler.AddBuyButtonEvent(ShopUIManager.Instance.UpdateCurrenciesIntoShopInfo);
                    if (InventoryManager.Instance.IsPurchased(item.itemID))
                    {
                        itemUIHandler.SetItemAsPurchased(item.isConsumable, item.isEquippable); // Mark the item as purchased
                        if (item.isEquippable)
                        {
                            itemUIHandler.SetActiveEquipButton(true);
                            if (InventoryManager.Instance.IsEquipped(item.itemID))
                            {
                                // If the item is already equipped, set the text to "Unequip" and set the listener to unequip the item
                                itemUIHandler.SetItemAsEquipped(() => itemsData.UnequipItem(index),item.itemType);
                            }
                            else
                            {
                                // If the item is not equipped, set the text to "Equip" and set the listener to equip the item
                                itemUIHandler.SetItemAsUnequipped(() => itemsData.EquipItem(index));
                            }

                            itemUIHandler.AddEquipButtonEvent(RefreshShopItems); // Add event to refresh shop items after equipping/unequipping
                        }
                        else
                        {
                            itemUIHandler.SetActiveEquipButton(false);
                        }
                    }
                    else
                    {                         
                        itemUIHandler.SetItemAsNotPurchased(); // Mark the item as not purchased and disable the equip button
                    }
                    if (item.isConsumable)
                    {
                        int itemQuantity = InventoryManager.Instance.inventory.GetItemQuantity(item.itemID);
                        itemUIHandler.SetQuantitySprite(itemQuantity); // Set the UI quantity for consumable items
                    }
                }
            }
        }
    }

    public void RefreshShopItems()
    {
        GenerateShopItems(); // Regenerate the shop items list
    }

}
