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
        if (playerStatusController.playerCurrentStatus != PlayerStatus.Dead)
        {
            //RailStatusHandler(other);
            playerGrindingScript.OnRailDetectEnter(other);
            InGameController.Instance.lastRailEntered = other.transform.parent.gameObject;  // Get the lastest rail entered
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerStatusController.playerCurrentStatus != PlayerStatus.Dead)
        {
            playerGrindingScript.OnRailDetectExit(other, frontDetector._hasRailInFront);
        }
    }

    private void RailStatusHandler(Collider other)
    {
        if (other.gameObject.name.Contains("R1"))
        {
            playerStatusController.playerCurrentRail = 1;
            List<Transform> r0 = GetChildsContains(other.transform.parent, "R0");
            if (r0.Count > 0)
            {
                playerStatusController.canJumpLeft = true;
            }
            else
            {
                playerStatusController.canJumpLeft = false;
            }
            List<Transform> r2 = GetChildsContains(other.transform.parent, "R2");
            if (r2.Count > 0)
            {
                playerStatusController.canJumpRight = true;
            }
            else
            {
                playerStatusController.canJumpRight = false;
            }
        }
        else if (other.gameObject.name.Contains("R0"))
        {
            playerStatusController.playerCurrentRail = 0;
            playerStatusController.canJumpLeft = false;
            List<Transform> r1 = GetChildsContains(other.transform.parent,"R1");
            if (r1.Count > 0)
            {
                playerStatusController.canJumpRight = true;
            }
            else
            {
                playerStatusController.canJumpRight = false;
            }
        }
        else if (other.gameObject.name.Contains("R2"))
        {
            playerStatusController.playerCurrentRail = 2;
            playerStatusController.canJumpRight = false;
            List<Transform> r1 = GetChildsContains(other.transform.parent, "R1");
            if (r1.Count > 0)
            {
                playerStatusController.canJumpLeft = true;
            }
            else
            {
                playerStatusController.canJumpLeft = false;
            }
        }
    }

    private List<Transform> GetChildsContains(Transform parent, string stringCheck)
    {
        List<Transform> childList = new List<Transform>();
        foreach (Transform child in parent)
        {
            if (child.name.Contains(stringCheck))
            {
                childList.Add(child);
            }
        }
        return childList;
    }
}
