using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDetector : MonoBehaviour
{
    public bool _hasRailInFront;
    public PlayerStatusController playerStatusController;
    [SerializeField] PlayerCartGrindMovement playerGrindingScript;

    Collider[] frontRails;

    private void Start()
    {
        frontRails = new Collider[3];
        _hasRailInFront = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Rail")
        {
            //Debug.Log("rail in front: " + other.transform.parent.name);
            _hasRailInFront = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!RailInRange(out frontRails))
        {
            if(playerStatusController.playerCurrentStatus != PlayerStatus.Jump)
            {
                Debug.Log("No rail in front detected!");
            }
            _hasRailInFront = false;
            //playerGrindingScript.EmptyCurrentRailScript();
            //playerGrindingScript.DeadEndJumpOffCliff();
        }
    }

    private bool RailInRange(out Collider[] overlapingColliders)
    {
        Collider frontCollider = GetComponent<Collider>();
        Vector3 center = frontCollider.bounds.center;
        Vector3 halfExtents = frontCollider.bounds.extents;
        Quaternion rotation = transform.rotation;
        overlapingColliders = Physics.OverlapBox(center, halfExtents, rotation);
        //Debug.Log("Number collider in front: " + overlapingColliders.Length);
        foreach (Collider collider in overlapingColliders)
        {
            if (collider.gameObject.tag == "Rail")
            {
                //Debug.Log("Has rail in front range: " + collider.transform.parent.name);
                return true;
            }
        }
        return false;
    }
}
