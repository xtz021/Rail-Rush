using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public string itemID; // Unique identifier for the item
    public Sprite iconImage;
    public string name;
    public string description;
    public Sprite descriptionImage;
    public int price;
    public ShopItemType itemType; // Type of the shop item
    public bool isIAP; // In-App Purchase item
    public bool isEquippable; // Indicates if the item can be equipped
    public bool isConsumable; // Indicates if the item is consumable (e.g., life, magnet)
    public int purchaseAmount = 1; // Amount to purchase, default is 1
}

public enum ShopItemType
{
    Chacter,
    Magnet,
    SecondChance,
    Pass,
    Nuggetx2,
    Gemx2,
    TNT,
    Brake,
    NoType
}
