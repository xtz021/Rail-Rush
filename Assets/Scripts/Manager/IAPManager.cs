using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using System.Collections.Generic;

public class IAPManager : MonoBehaviour, IDetailedStoreListener
{
    public static IAPManager Instance;

    private IStoreController storeController;
    private IExtensionProvider extensionProvider;

    [SerializeField] ShopItemsData characterShopData;
    [SerializeField] ShopItemsData cartStuffShopData;
    [SerializeField] ShopItemsData extraShopData;
    [SerializeField] ShopItemsData nuggetShopData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        if (IsInitialized()) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        foreach (var item in characterShopData.shopItems)
        {
            if (item.isIAP)
            {
                builder.AddProduct(item.iapID, ProductType.Consumable);
            }
        }
        foreach (var item in cartStuffShopData.shopItems)
        {
            if (item.isIAP)
            {
                builder.AddProduct(item.iapID, ProductType.Consumable);
            }
        }
        foreach (var item in extraShopData.shopItems)
        {
            if (item.isIAP)
            {
                builder.AddProduct(item.iapID, ProductType.Consumable);
            }
        }
        foreach (var item in nuggetShopData.shopItems)
        {
            if (item.isIAP)
            {
                builder.AddProduct(item.iapID, ProductType.Consumable);
            }
        }

        UnityPurchasing.Initialize(this as IDetailedStoreListener, builder);
    }

    private bool IsInitialized()
    {
        return storeController != null && extensionProvider != null;
    }

    public void BuyProduct(string productId)
    {
        if (!IsInitialized())
        {
            Debug.LogError("IAP not initialized.");
            return;
        }

        Product product = storeController.products.WithID(productId);

        if (product != null && product.availableToPurchase)
        {
            storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.LogError("Product not found or not available.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
        Debug.Log("IAP Initialized");
    }

    public void OnInitializeFailed(InitializationFailureReason error) =>
        Debug.LogError("IAP Initialization Failed: " + error);

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        foreach (var item in characterShopData.shopItems)
        {
            if (item.iapID == args.purchasedProduct.definition.id)
            {
                Debug.Log("Purchase successful: " + item.iapID);
                InventoryManager.Instance.inventory.AddItem(item.itemID, item.purchaseAmount);
                GameMenuUIController.Instance.PopUpNotice("Purchase successful!");
                break;
            }
        }
        foreach (var item in cartStuffShopData.shopItems)
        {
            if (item.iapID == args.purchasedProduct.definition.id)
            {
                Debug.Log("Purchase successful: " + item.iapID);
                InventoryManager.Instance.inventory.AddItem(item.itemID, item.purchaseAmount);
                GameMenuUIController.Instance.PopUpNotice("Purchase successful!");
                break;
            }
        }
        foreach (var item in extraShopData.shopItems)
        {
            if (item.iapID == args.purchasedProduct.definition.id)
            {
                Debug.Log("Purchase successful: " + item.iapID);
                InventoryManager.Instance.inventory.AddItem(item.itemID, item.purchaseAmount);
                GameMenuUIController.Instance.PopUpNotice("Purchase successful!");
                break;
            }
        }
        foreach (var item in extraShopData.shopItems)
        {
            if (item.iapID == args.purchasedProduct.definition.id)
            {
                Debug.Log("Purchase successful: " + item.iapID);
                InventoryManager.Instance.inventory.AddItem(item.itemID, item.purchaseAmount);
                GameMenuUIController.Instance.PopUpNotice("Purchase successful!");
                break;
            }
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogWarning($"Purchase failed: {product.definition.id}, Reason: {failureReason}");
        GameMenuUIController.Instance.PopUpNotice($"Purchase failed: {failureReason}");
    }
        

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    void IDetailedStoreListener.OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        throw new System.NotImplementedException();
    }
}
