using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailDetector : MonoBehaviour
{
    [SerializeField] PlayerCartGrindMovement playerGrindingScript;
    [SerializeField] FrontDetector frontDetector;

    private void OnTriggerEnter(Collider other)
    {
        playerGrindingScript.OnRailDetectEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        playerGrindingScript.OnRailDetectExit(other, frontDetector._hasRailInFront);
    }

}
