using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public static ShopUIManager Instance { get; private set; }

    [Header("Shop Panels")]
    [SerializeField] GameObject mainShop;
    [SerializeField] GameObject heroesShop;
    [SerializeField] GameObject cartStuffShop;
    [SerializeField] GameObject extrasShop;
    [SerializeField] GameObject nuggetsShop;

    [Header("Gold and Pass Ticket Count UI Texts for Shop Info")]
    public List<Text> goldTexts;
    public List<Text> passCountTexts;

    [Header("Shop Buttons")]
    [SerializeField] List<Button> returnToMainShopButtons;
    [SerializeField] Button heroesShopButton;
    [SerializeField] Button cartStuffShopButton;
    [SerializeField] Button extrasShopButton;
    [SerializeField] Button nuggetsShopButton;

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
    private void Start()
    {
        AddShopButtonEvents();
    }

    private void OnEnable()
    {
        UpdateCurrenciesIntoShopInfo();
    }

    private void OnDisable()
    {
        InventoryManager.Instance.SaveInventory(); // Save inventory when shop UI is closed
    }

    public void UpdateCurrenciesIntoShopInfo()
    {
        foreach (Text goldText in goldTexts)
        {
            goldText.text = "" + InventoryManager.Instance.inventory.Gold;
        }
        foreach (Text passCountText in passCountTexts)
        {
            passCountText.text = "" + InventoryManager.Instance.inventory.PassTickets;
        }
    }


    public void AddShopButtonEvents()
    { 
        if (returnToMainShopButtons != null)
        {
            foreach (Button returnButton in returnToMainShopButtons)
            {
                if (returnButton != null)
                {
                    returnButton.onClick.RemoveAllListeners(); // Clear previous listeners to avoid duplicates
                    returnButton.onClick.AddListener(ReturnToMainShop);
                }
            }
        }
        if (heroesShopButton != null)
        {
            heroesShopButton.onClick.RemoveAllListeners();
            heroesShopButton.onClick.AddListener(OpenHeroesShop);
        }
        if (cartStuffShopButton != null)
        {
            cartStuffShopButton.onClick.RemoveAllListeners();
            cartStuffShopButton.onClick.AddListener(OpenCartStuffShop);
        }
        if (extrasShopButton != null)
        {
            extrasShopButton.onClick.RemoveAllListeners();
            extrasShopButton.onClick.AddListener(OpenExtrasShop);
        }
        if(nuggetsShopButton != null)
        {
            nuggetsShopButton.onClick.RemoveAllListeners();
            nuggetsShopButton.onClick.AddListener (OpenNuggetsShop);
        }
    }

    public void ReturnToMainShop()
    {
        mainShop.SetActive(true);
        heroesShop.SetActive(false);
        cartStuffShop.SetActive(false);
        extrasShop.SetActive(false);
        nuggetsShop.SetActive(false);
    }

    public void OpenHeroesShop()
    {
        mainShop.SetActive(false);
        heroesShop.SetActive(true);
        cartStuffShop.SetActive(false);
        extrasShop.SetActive(false);
        nuggetsShop.SetActive(false);
    }

    public void OpenCartStuffShop()
    {
        mainShop.SetActive(false);
        heroesShop.SetActive(false);
        cartStuffShop.SetActive(true);
        extrasShop.SetActive(false);
        nuggetsShop.SetActive(false);
    }

    public void OpenExtrasShop()
    {
        mainShop.SetActive(false);
        heroesShop.SetActive(false);
        cartStuffShop.SetActive(false);
        extrasShop.SetActive(true);
        nuggetsShop.SetActive(false);
    }

    public void OpenNuggetsShop()
    {
        mainShop.SetActive(false);
        heroesShop.SetActive(false);
        cartStuffShop.SetActive(false);
        extrasShop.SetActive(false);
        nuggetsShop.SetActive(true);
    }

}
