using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIDsContainers : MonoBehaviour
{
    public static ItemIDsContainers Instance { get; private set; }

    public ShopItemsData shopCartStuffsData;
    public ShopItemsData shopExtrasData;
    public ShopItemsData shopHeroesData;

    [HideInInspector] public string itemID_MagnetStandard;
    [HideInInspector] public string itemID_MagnetSuper;
    [HideInInspector] public string itemID_MagnetMega;

    [HideInInspector] public string itemID_SecondChance;
    [HideInInspector] public string itemID_NuggetX2;

    [HideInInspector] public string itemID_Char1;
    [HideInInspector] public string itemID_Char2;
    [HideInInspector] public string itemID_Char3;

    private void Awake()
    {
        if (Instance != null &&  Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        AssignIDs();
    }

    private void AssignIDs()
    {
        itemID_MagnetStandard = GetCartItemIDByIndex(0);
        itemID_MagnetSuper = GetCartItemIDByIndex(1);
        itemID_MagnetMega = GetCartItemIDByIndex(2);
        itemID_SecondChance = GetExtraItemIDByName("Second Chance");
        itemID_NuggetX2 = GetExtraItemIDByName("Nuggets x2");
        itemID_Char1 = GetCharacterIDByIndex(0);
        itemID_Char2 = GetCharacterIDByIndex(1);
        itemID_Char3 = GetCharacterIDByIndex(2);
    }

    public string GetCharacterIDByIndex(int index)
    {
        if (index < 0 || index >= shopHeroesData.ItemsCount)
        {
            Debug.LogError("Index out of range: " + index);
            return null;
        }
        return shopHeroesData.GetItem(index).itemID;
    }

    public string GetCharacterIDByName(string name)
    {
        foreach (ShopItem item in shopHeroesData.shopItems)
        {
            if (item.name.Contains(name))
            {
                return item.itemID;
            }
        }
        Debug.LogError("Character with name " + name + " not found in shopHeroesData.");
        return null;
    }


    public string GetCartItemIDByIndex(int index)
    {
        if (index < 0 || index >= shopCartStuffsData.ItemsCount)
        {
            Debug.LogError("Index out of range: " + index);
            return null;
        }
        return shopCartStuffsData.GetItem(index).itemID;
    }

    public string GetCartItemIDByName(string name)
    {
        foreach (ShopItem item in shopCartStuffsData.shopItems)
        {
            if (item.name.Contains(name))
            {
                return item.itemID;
            }
        }
        Debug.LogError("Cart item with name " + name + " not found in shopCartStuffsData.");
        return null;
    }

    public string GetExtraItemIDByIndex(int index)
    {
        if (index < 0 || index >= shopExtrasData.ItemsCount)
        {
            Debug.LogError("Index out of range: " + index);
            return null;
        }
        return shopExtrasData.GetItem(index).itemID;
    }

    public string GetExtraItemIDByName(string name)
    {
        foreach (ShopItem item in shopExtrasData.shopItems)
        {
            if (item.name.Contains(name))
            {
                return item.itemID;
            }
        }
        Debug.LogError("Extra item with name " + name + " not found in shopExtrasData.");
        return null;
    }
}
