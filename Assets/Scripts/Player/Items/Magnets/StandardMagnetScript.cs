using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMagnetScript : MagnetBase
{
    float attractionRange = 15f; // The range within which the magnet can attract items.

    string nuggetNameTag = "Nugget"; // The name of the bonus item to be attracted


    public override List<GameObject> AttractedItems()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attractionRange);
        List<GameObject> nearbyNuggets = new List<GameObject>();

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Bonus") && hit.gameObject.name.Contains(nuggetNameTag)) // Assuming bonus items have the tag "BonusItem"
            {
                nearbyNuggets.Add(hit.gameObject);
            }
        }
        return nearbyNuggets;
    }


}
