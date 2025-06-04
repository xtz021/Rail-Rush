using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInfoTableManager : MonoBehaviour
{
    public static ShopInfoTableManager Instance { get; private set; }
    [Header("Shop Panels")]
    [SerializeField] GameObject mainShop;
    [SerializeField] GameObject heroesShop;
    [SerializeField] GameObject cartStuffShop;
    [SerializeField] GameObject extrasShop;

    [Header("Gold and Pass Ticket Count UI Texts for Shop Info")]
    public List<Text> goldTexts;
    public List<Text> passCountTexts;

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

    private void OnEnable()
    {
        UpdateShopInfo();
    }

    public void UpdateShopInfo()
    {
        foreach (Text goldText in goldTexts)
        {
            goldText.text = "" + PlayerInventoryManager.Instance.playerInventorySO.Gold;
        }
        foreach (Text passCountText in passCountTexts)
        {
            passCountText.text = "" + PlayerInventoryManager.Instance.playerInventorySO.PassTicket;
        }
    }

    public void ReturnToMainShop()
    {
        mainShop.SetActive(true);
        heroesShop.SetActive(false);
        cartStuffShop.SetActive(false);
        extrasShop.SetActive(false);
    }

    public void OpenHeroesShop()
    {
        mainShop.SetActive(false);
        heroesShop.SetActive(true);
        cartStuffShop.SetActive(false);
        extrasShop.SetActive(false);
    }

    public void OpenCartStuffShop()
    {
        mainShop.SetActive(false);
        heroesShop.SetActive(false);
        cartStuffShop.SetActive(true);
        extrasShop.SetActive(false);
    }

    public void OpenExtrasShop()
    {
        mainShop.SetActive(false);
        heroesShop.SetActive(false);
        cartStuffShop.SetActive(false);
        extrasShop.SetActive(true);
    }
}
