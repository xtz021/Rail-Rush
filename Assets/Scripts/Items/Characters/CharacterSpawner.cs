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
                return; // Exit after spawning the equipped character
            }
        }
        Debug.LogError("No equipped character found to spawn.");
    }
}
