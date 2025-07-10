using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private ShopItemsData shopHeroesData; // Data for characters

    public void SpawnEquipedCharacter()
    {
        foreach (ShopItem item in shopHeroesData.shopItems)
        {
            if (InventoryManager.Instance.IsEquipped(item.itemID))
            {
                GameObject characterPrefab = item.prefab;
                if (characterPrefab != null)
                {
                    Instantiate(characterPrefab, transform.position, Quaternion.identity, transform);
                    Debug.Log("Spawned equipped character: " + item.name);
                }
                else
                {
                    Debug.LogError("Character prefab is not assigned for item: " + item.name);
                }
                int charIndex = shopHeroesData.shopItems.IndexOf(item) + 1;
                CartAnimationController.Instance.SetCurrentCharacterIndex(charIndex); // Set the current character index in the CartAnimationController
                return; // Exit after spawning the equipped character
            }
        }
        Debug.LogError("No equipped character found to spawn.");
        ShopItem defaultCharacter = shopHeroesData.shopItems[0]; 
        InventoryManager.Instance.EquipItem(defaultCharacter.itemID, defaultCharacter.itemType);    // Equip the default character if no equipped character is found
        // Spawn the default character
        GameObject defaultCharacterPrefab = defaultCharacter.prefab;
        if (defaultCharacterPrefab != null)
        {
            Instantiate(defaultCharacterPrefab, transform.position, Quaternion.identity, transform);
            Debug.Log("Spawned default character: " + defaultCharacter.name);
        }
        else
        {
            Debug.LogError("Default character prefab is not assigned for item: " + defaultCharacter.name);
        }

    }
}
