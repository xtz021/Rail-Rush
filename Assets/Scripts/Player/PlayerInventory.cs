using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    public const string KEY_PASSTICKET_COUNT = "PassTicketCount";
    public const string KEY_SECONDCHANCE_COUNT = "SecondChanceCount";
    public const string KEY_DOUBLENUGGET_UNLOCKED = "IsDoubleNuggetUnlocked";
    public const string KEY_MAGNET_TYPE_EQUIPED = "Magnet_Type_Equiped";
    public const string KEY_MAGNET_STANDARD_USECOUNT_INT = "Magnet_Standard_UseCount";
    public const string KEY_MAGNET_SUPER_USECOUNT_INT = "Magnet_Super_UseCount";
    public const string KEY_MAGNET_MEGA_USECOUNT_INT = "Magnet_Mega_UseCount";


    public int passTicketCount;
    public int secondChanceCount;
    public bool isDoubleNuggetUnlocked;
    public MagnetType magnetEquiped;
    public int magnetStandardUseCount;
    public int magnetSuperUseCount;
    public int magnetMegaUseCount;

    private PlayerInventory() { }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        SaveInventoryData();
    }

    private void Start()
    {
        GetInventorySaveData();
    }

    private void GetInventorySaveData()
    {
        passTicketCount = PlayerPrefs.GetInt(KEY_PASSTICKET_COUNT, 0);
        secondChanceCount = PlayerPrefs.GetInt(KEY_SECONDCHANCE_COUNT, 0);
        isDoubleNuggetUnlocked = PlayerPrefs.GetInt(KEY_DOUBLENUGGET_UNLOCKED, 0) == 1;
        int magnetTypeInt = PlayerPrefs.GetInt(KEY_MAGNET_TYPE_EQUIPED, (int)MagnetType.None);  // Save magnet type as int
        magnetEquiped = (MagnetType)magnetTypeInt;
        magnetStandardUseCount = PlayerPrefs.GetInt(KEY_MAGNET_STANDARD_USECOUNT_INT, 0);
        magnetSuperUseCount = PlayerPrefs.GetInt(KEY_MAGNET_SUPER_USECOUNT_INT, 0);
        magnetMegaUseCount = PlayerPrefs.GetInt(KEY_MAGNET_MEGA_USECOUNT_INT, 0);
    }

    public void SaveInventoryData()         // Is called when GameOverPanel pop up
    {
        PlayerPrefs.SetInt(KEY_PASSTICKET_COUNT, passTicketCount);
        PlayerPrefs.SetInt(KEY_SECONDCHANCE_COUNT, secondChanceCount);
        PlayerPrefs.SetInt(KEY_DOUBLENUGGET_UNLOCKED, isDoubleNuggetUnlocked ? 1 : 0);
        int magnetTypeInt = (int)magnetEquiped;                                             // Convert magnet type to int for saving
        PlayerPrefs.SetInt(KEY_MAGNET_TYPE_EQUIPED, magnetTypeInt);
        PlayerPrefs.SetInt(KEY_MAGNET_STANDARD_USECOUNT_INT, magnetStandardUseCount);
        PlayerPrefs.SetInt(KEY_MAGNET_SUPER_USECOUNT_INT, magnetSuperUseCount);
        PlayerPrefs.SetInt(KEY_MAGNET_MEGA_USECOUNT_INT, magnetMegaUseCount);
        PlayerPrefs.Save();
    }
}

public enum MagnetType
{
    None,
    Standard,
    Super,
    Mega
}

public enum CartPartType
{
    None,
    Wood,
    Iron,
    Steel
}
