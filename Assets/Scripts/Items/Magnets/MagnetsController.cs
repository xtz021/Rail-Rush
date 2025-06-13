using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetsController : MonoBehaviour
{
    [SerializeField] private GameObject standardMagnetPrefab;
    [SerializeField] private GameObject superMagnetPrefab;
    [SerializeField] private GameObject megaMagnetPrefab;

    List<PlayerInventorySO.InventoryItem> inventoryItems;

    private void Start()
    {
        inventoryItems = InventoryManager.Instance.inventory.items;
        SpawnMagnets();
    }

    private void SpawnMagnets()
    {
        foreach (var item in inventoryItems)
        {
            if(item.isEquipped)
            {
                if (item.itemID == ItemIDsContainers.Instance.itemID_MagnetStandard && item.quantity > 0)
                {
                    Instantiate(standardMagnetPrefab, Vector3.zero, Quaternion.identity, transform);
                    Debug.Log("Standard Magnet Spawned");
                    return;
                }
                else if (item.itemID == ItemIDsContainers.Instance.itemID_MagnetSuper && item.quantity > 0)
                {
                    Instantiate(superMagnetPrefab, Vector3.zero, Quaternion.identity, transform);
                    Debug.Log("Super Magnet Spawned");
                    return;
                }
                else if (item.itemID == ItemIDsContainers.Instance.itemID_MagnetMega && item.quantity > 0)
                {
                    Instantiate(megaMagnetPrefab, Vector3.zero, Quaternion.identity, transform);
                    Debug.Log("Mega Magnet Spawned");
                    return;
                }
            }
        }
        Debug.Log("No magnets equipped or available in inventory.");
    }
}
