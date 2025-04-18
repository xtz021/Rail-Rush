using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailDetector : MonoBehaviour
{
    [SerializeField] PlayerCartGrindMovement playerGrindingScript;
    [SerializeField] FrontDetector frontDetector;
    [SerializeField] PlayerStatusController playerStatusController;

    private void OnTriggerEnter(Collider other)
    {
        if(playerStatusController.playerCurrentStatus != PlayerStatus.Dead)
        {
            playerGrindingScript.OnRailDetectEnter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerStatusController.playerCurrentStatus != PlayerStatus.Dead)
        {
            playerGrindingScript.OnRailDetectExit(other, frontDetector._hasRailInFront);
        }
    }

}
