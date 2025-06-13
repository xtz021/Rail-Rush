using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIDsContainers : MonoBehaviour
{
    public static ItemIDsContainers Instance { get; private set; }

    [SerializeField] ShopItemsData shopCartStuffsData;
    [SerializeField] ShopItemsData shopExtrasData;
    [SerializeField] ShopItemsData shopHeroesData;

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
        itemID_MagnetStandard = shopCartStuffsData.GetItem(0).itemID;
        itemID_MagnetSuper = shopCartStuffsData.GetItem(1).itemID;
        itemID_MagnetMega = shopCartStuffsData.GetItem(2).itemID;
        itemID_SecondChance = shopExtrasData.GetItem(0).itemID;
        itemID_NuggetX2 = shopExtrasData.GetItem(2).itemID;
        itemID_Char1 = shopHeroesData.GetItem(0).itemID;
        itemID_Char2 = shopHeroesData.GetItem(1).itemID;
        itemID_Char3 = shopHeroesData.GetItem(2).itemID;
    }
}
