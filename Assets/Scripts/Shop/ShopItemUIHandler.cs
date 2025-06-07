using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemUIHandler : MonoBehaviour
{
    [SerializeField] Image iconImage;
    [SerializeField] Image quantityImg;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] Image descriptionImage;
    [SerializeField] TMP_Text priceText;
    [SerializeField] Button purchaseButton;
    [SerializeField] Button equipButton;
    [SerializeField] TMP_Text equipButtonText;

    [Space (20f)]
    [SerializeField] Button itemButton;
    [SerializeField] GameObject dropDownField;

    [Space(20f)]
    [SerializeField] List<Sprite> quantitySprites;

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

    public void SetDescription(string description)
    {
        if (descriptionText != null)
        {
            descriptionText.text = description;
        }
    }

    public void SetDescriptionImage(Sprite descriptionSprite)
    {
        if (descriptionImage != null)
        {
            descriptionImage.sprite = descriptionSprite;
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

    public void SetQuantity(int quantity)
    {
        if (quantityImg != null && quantitySprites != null && quantitySprites.Count > 0)
        {
            quantityImg.gameObject.SetActive(true);
            if (quantity > 0 && quantity < quantitySprites.Count)
            {
                quantityImg.sprite = quantitySprites[quantity];
            }
            else if(quantity > 20)
            {
                quantityImg.sprite = quantitySprites[0];
            }
            else
            {
                quantityImg.gameObject.SetActive(false);
                Debug.LogWarning("Quantity out of range for sprites list.");
            }
        }
        else
        {
            Debug.LogWarning("Quantity image or sprites list is not set.");
        }
    }

    public void DropDown()
    {
        Animator animator = dropDownField.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("OnClickDropDown");
            if (dropDownField.transform.IsChildOf(itemButton.transform))
            {
                dropDownField.transform.SetParent(itemButton.transform.parent);
                dropDownField.transform.SetSiblingIndex(itemButton.transform.GetSiblingIndex()+1);
            }
            else
            {
                dropDownField.transform.SetParent(itemButton.transform);
            }
        }
    }

    public void SetBuyButtonEvent(UnityAction action)
    {
        if (purchaseButton != null)
        {
            purchaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.AddListener(action);
        }
    }

    public void SetEquipButtonEvent(UnityAction action)
    {
        if (equipButton != null)
        {
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(action);
        }
    }

    public void SetItemAsPurchased(bool isConsumable, bool isEquipable)
    {
        if (purchaseButton != null)
        {
            if (isConsumable)
            {
                purchaseButton.gameObject.SetActive(true);
            }
            else
            {
                purchaseButton.gameObject.SetActive(false);
                priceText.text = "Owned";
            }
            if (isEquipable)
            {
                equipButton.gameObject.SetActive(true);
                equipButtonText.text = "Equip";
            }
            else
            {
                equipButton.gameObject.SetActive(false);
            }
        }
    }

    public void SetItemAsNotPurchased()
    {
        if (purchaseButton != null)
        {
            purchaseButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(false);
        }
    }


    public void SetActiveEquipButton(bool active)
    {
        equipButton.gameObject.SetActive(active);
    }


    public void SetItemAsEquipped(UnityAction unequipAction, ShopItemType itemType)
    {
        if (equipButton != null && itemType != ShopItemType.Chacter)
        {
            equipButtonText.text = "Unequip";
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(unequipAction);
            equipButton.interactable = true; // Enable the button for unequipping
        }
        else
        {
            equipButtonText.text = "Equipped";
            equipButton.onClick.RemoveAllListeners();
            equipButton.interactable = false; // Disable the button if it's a character item
        }
    }

    public void SetItemAsUnequipped(UnityAction equipAction)
    {
        if (equipButton != null)
        {
            equipButtonText.text = "Equip";
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(equipAction);
            equipButton.interactable = true; // Enable the button for equipping
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
