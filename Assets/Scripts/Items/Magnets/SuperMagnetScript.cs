using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMagnetScript : MagnetBase
{
    float attractionRange = 15f; // The range within which the magnet can attract items.


    public override List<GameObject> AttractedItems()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attractionRange);
        List<GameObject> nearbyBonus = new List<GameObject>();

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Bonus")) // Assuming bonus items have the tag "BonusItem"
            {
                nearbyBonus.Add(hit.gameObject);
            }
        }
        return nearbyBonus;
    }
}
