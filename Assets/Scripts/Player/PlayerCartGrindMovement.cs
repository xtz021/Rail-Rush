using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.Splines;

public class PlayerCartGrindMovement : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] bool jump;         //Inputs aren't used in the tutorial
    [SerializeField] Vector3 input;     //But they're here for rail switching

    [Header("Variables")]
    public bool onRail;
    [SerializeField] float grindSpeed;
    [SerializeField] float heightOffset;
    float timeForFullSpline;
    float elapsedTime;
    [SerializeField] float lerpSpeed = 15f;

    [Header("Scripts")]
    [SerializeField] RailScript currentRailScript;

    Rigidbody playerRigidbody;
    PlayerStatusController playerStatusController;
    PlayerCartMovement playerCartMovement;
    private Collider[] overlapingColliders;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerStatusController = GetComponent<PlayerStatusController>();
        playerCartMovement = GetComponent<PlayerCartMovement>();
    }
    public void HandleJump(InputAction.CallbackContext context)
    {
        jump = Convert.ToBoolean(context.ReadValue<float>());
    }
    public void HandleMovement(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        input.x = rawInput.x;
    }
    private void FixedUpdate()
    {
        if (onRail && playerStatusController.playerCurrentStatus != PlayerStatus.Jump) //If on the rail, move the player along the rail
        {
            MovePlayerAlongRail();
        }
    }
    private void Update()
    {

    }
    void MovePlayerAlongRail()
    {
        if (currentRailScript != null && onRail) //This is just some additional error checking.
        {
            //Calculate a 0 to 1 normalised time value which is the progress along the rail.
            //Elapsed time divided by the full time needed to traverse the spline will give you that value.
            float progress = elapsedTime / timeForFullSpline;

            //If progress is less than 0, the player's position is before the start of the rail.
            //If greater than 1, their position is after the end of the rail.
            //In either case, the player has finished their grind.
            if (progress < 0 || progress > 1)
            {
                ThrowOffRail();
                return;
            }

            float nextTimeNormalised;
            if (currentRailScript.normalDir)
                nextTimeNormalised = (elapsedTime + Time.deltaTime) / timeForFullSpline;
            else
                nextTimeNormalised = (elapsedTime - Time.deltaTime) / timeForFullSpline;

            float3 pos, tangent, up;
            float3 nextPosfloat, nextTan, nextUp;
            SplineUtility.Evaluate(currentRailScript.railSpline.Spline, progress, out pos, out tangent, out up);
            SplineUtility.Evaluate(currentRailScript.railSpline.Spline, nextTimeNormalised, out nextPosfloat, out nextTan, out nextUp);

            //Converting the local positions into world positions.
            Vector3 worldPos = currentRailScript.LocalToWorldConversion(pos);
            Vector3 nextPos = currentRailScript.LocalToWorldConversion(nextPosfloat);

            //Setting the player's position and adding a height offset so that they're sitting on top of the rail instead of being in the middle of it.
            transform.position = worldPos + (transform.up * heightOffset);
            //Lerping the player's current rotation to the direction of where they are to where they're going.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(nextPos - worldPos), lerpSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0,0,1)), lerpSpeed * Time.deltaTime);
            //Lerping the player's up direction to match that of the rail, in relation to the player's current rotation.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, up), lerpSpeed * Time.deltaTime);
            //transform.LookAt(tangent);
            float z = transform.rotation.eulerAngles.z;
            if(z > 50 ||  z < -50)
            {
                Debug.Log("Rotation z: " + z);
                Debug.Log("Position: " + transform.position);
            }

            //Finally incrementing or decrementing elapsed time for the next update based on direction.
            if (currentRailScript.normalDir)
                elapsedTime += Time.deltaTime;
            else
                elapsedTime -= Time.deltaTime;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Rail")
        {
            if (currentRailScript != collision.gameObject.GetComponent<RailScript>() && currentRailScript != null)
            {
                return;
            }
            if(playerStatusController.playerCurrentStatus == PlayerStatus.OnRail)
            {
                playerStatusController.playerCurrentStatus = PlayerStatus.OffRail;
            }
            Debug.Log("Exit rail: " + collision.transform.parent.name);
            //playerRigidbody.useGravity = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision enter: " + collision.transform.parent.name);
        if (collision.gameObject.tag == "Rail")
        {
            playerStatusController.playerCurrentStatus = PlayerStatus.OnRail;
            playerRigidbody.useGravity = false;
            playerCartMovement.StopJumpingCoroutines();
            /*When the player hits the rail, onRail is set to true, the current rail script is set to the
             *rail script of the rail the player hits. Then we calculate the player's position on that rail.
            */
            onRail = true;
            currentRailScript = collision.gameObject.GetComponent<RailScript>();
            CalculateAndSetRailPosition();
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Rail")
    //    {
    //        if (playerStatusController.playerCurrentStatus == PlayerStatus.OnRail)
    //        {
    //            playerStatusController.playerCurrentStatus = PlayerStatus.OffRail;
    //        }
    //        Debug.Log("Exit rail: " + other.transform.parent.name);
    //        //playerRigidbody.useGravity = true;
    //    }
    //}


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Rail")
    //    {
    //        playerStatusController.playerCurrentStatus = PlayerStatus.OnRail;
    //        playerRigidbody.useGravity = false;
    //        playerCartMovement.StopJumpingCoroutines();
    //        /*When the player hits the rail, onRail is set to true, the current rail script is set to the
    //         *rail script of the rail the player hits. Then we calculate the player's position on that rail.
    //        */
    //        onRail = true;
    //        currentRailScript = other.gameObject.GetComponent<RailScript>();
    //        CalculateAndSetRailPosition();
    //    }
    //}

    void CalculateAndSetRailPosition()
    {
        //Figure out the amount of time it would take for the player to cover the rail.
        timeForFullSpline = currentRailScript.totalSplineLength / grindSpeed;

        //This is going to be the world position of where the player is going to start on the rail.
        Vector3 splinePoint;

        //The 0 to 1 value of the player's position on the spline. We also get the world position of where that
        //point is.
        float normalisedTime = currentRailScript.CalculateTargetRailPoint(transform.position, out splinePoint);
        elapsedTime = timeForFullSpline * normalisedTime;
        //Multiply the full time for the spline by the normalised time to get elapsed time. This will be used in
        //the movement code.

        //Spline evaluate takes the 0 to 1 normalised time above, 
        //and uses it to give you a local position, a tangent (forward), and up
        float3 pos, forward, up;
        SplineUtility.Evaluate(currentRailScript.railSpline.Spline, normalisedTime, out pos, out forward, out up);

        //Calculate the direction the player is going down the rail
        currentRailScript.CalculateDirection(forward, transform.forward);

        //Set player's initial position on the rail before starting the movement code.
        transform.position = splinePoint + (transform.up * heightOffset);
    }
    void ThrowOffRail()
    {
        //Set onRail to false, clear the rail script, and push the player off the rail.
        //It's a little sudden, there might be a better way of doing using coroutines and looping, but this will work.
        onRail = false;
        currentRailScript = null;
        if(playerStatusController.playerCurrentStatus != PlayerStatus.Jump)
        {
            playerStatusController.playerCurrentStatus = PlayerStatus.OffRail;
            transform.position += transform.forward * 1;
            playerRigidbody.useGravity = true;
        }
        Debug.Log("Thrown off rail");
    }

    private bool IsStillGrinding(out Collider[] overlapingColliders)
    {
        Collider grindingCollider = GetComponent<Collider>();
        Vector3 center = grindingCollider.bounds.center;
        Vector3 halfExtents = grindingCollider.bounds.extents;
        Quaternion rotation = transform.rotation;
        overlapingColliders = Physics.OverlapBox(center, halfExtents, rotation);
        foreach(Collider collider in overlapingColliders)
        {
            if(collider.gameObject.tag == "Rail")
            {
                return true;
            }
        }

        return false;
    }

}
