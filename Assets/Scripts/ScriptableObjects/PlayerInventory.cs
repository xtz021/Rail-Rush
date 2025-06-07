using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Player Inventory", order = 1)]
public class PlayerInventory : ScriptableObject
{
    public int Gold;
    public int PassTicket;
    public int SecondChance;
    public bool IsDoubleNuggetUnlocked;
    public MagnetType MagnetEquiped;
    public int MagnetStandard;
    public int MagnetSuper;
    public int MagnetMega;

    public void SetGoldValue(int value)
    {
        Gold = value;
    }
}

public class InventoryItem
{
    public string Name;
    public int Quantity;
    public bool IsPurchased;
    public bool IsEquipped;
    public InventoryItem(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
        IsPurchased = false;
        IsEquipped = false;
    }
}
