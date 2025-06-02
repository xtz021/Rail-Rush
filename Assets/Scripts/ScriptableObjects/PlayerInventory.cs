using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Object/Player Inventory", order = 1)]
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
