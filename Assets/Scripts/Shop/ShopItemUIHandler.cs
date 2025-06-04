using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemUIHandler : MonoBehaviour
{
    [SerializeField] Image iconImage;
    [SerializeField] TMP_Text nameText;
    //[SerializeField] TextMeshPro descriptionText;
    [SerializeField] TMP_Text priceText;
    [SerializeField] Button purchaseButton;

    [Space (10f)]
    [SerializeField] Button itemButton;

    private string priceCurrency_gold = "<sprite index=0>";
    private string priceCurrency_iap = "đ";

    public void SetIcon(Sprite icon)
    {
        if (iconImage != null)
        {
            iconImage.sprite = icon;
        }
    }

    public void SetName(string name)
    {
        if (nameText != null)
        {
            nameText.text = name;
        }
    }

    public void SetPrice(int price, bool isIAP)
    {
        if (priceText != null)
        {
            if (isIAP)
            {
                priceText.text =  price.ToString() + priceCurrency_iap;
            }
            else
            {
                priceText.text = price.ToString() + priceCurrency_gold;
            }
        }
    }

    public void SetItemAsPurchase()
    {
        if (purchaseButton != null)
        {
            purchaseButton.gameObject.SetActive(false);
        }
    }

    public void OnItemPurchase(int itemIndex, UnityAction<int> action)
    {
        if (purchaseButton != null)
        {
            purchaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
        }
    }


}
