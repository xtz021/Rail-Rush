using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagnetBase : MonoBehaviour
{
    float pullSpeed = 12f; // The speed at which items are pulled towards the magnet
    public abstract List<GameObject> AttractedItems(); // Function to return a list of items that are currently being attracted by the magnet.

    protected void Update()
    {
        PullingBonusItems();
    }

    protected void PullingBonusItems()
    {
        Vector3 playerPosition = PlayerCartMovement.Instance.transform.position;
        List<GameObject> bonusItems = AttractedItems();
        foreach (GameObject item in bonusItems)
        {
            // Calculate the direction towards the magnet
            Vector3 direction = (playerPosition - item.transform.position).normalized;
            // Move the item towards the magnet
            item.transform.position += direction * pullSpeed * Time.deltaTime;
        }
    }
}
