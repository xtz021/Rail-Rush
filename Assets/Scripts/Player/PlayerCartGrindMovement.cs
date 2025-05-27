using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
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

    [Header("Scripts that need input")]
    [SerializeField] FrontDetector frontDetector;
    [SerializeField] CartAnimationController cartAnimationController;

    //Rigidbody playerRigidbody;
    PlayerGravitySimulator gravitySim;
    PlayerStatusController playerStatusController;
    PlayerCartMovement playerCartMovement;
    private Collider[] overlapingColliders;
    private Transform cartTranform;

    private void Start()
    {
        gravitySim = GetComponent<PlayerGravitySimulator>();
        //playerRigidbody = GetComponent<Rigidbody>();
        playerStatusController = GetComponent<PlayerStatusController>();
        playerCartMovement = GetComponent<PlayerCartMovement>();
        cartTranform = cartAnimationController.gameObject.transform;
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
    private void Update()
    {
        if (playerStatusController.playerCurrentStatus != PlayerStatus.Dead)
        {
            if (onRail && playerStatusController.playerCurrentStatus != PlayerStatus.Jump) //If on the rail and not jumping, move the player along the rail
            {
                MovePlayerAlongRail();
            }
        }
        else
        {
            currentRailScript = null;
        }
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
            if ((progress < 0 || progress > 1) && !frontDetector._hasRailInFront
                && playerStatusController.playerCurrentStatus != PlayerStatus.Jump)
            {
                Debug.Log($"Deadend of {currentRailScript.transform.parent.name}!!!");
                DeadEndJumpOffCliff();
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

            if (Vector3.Distance(nextPos, worldPos) == 0 && frontDetector._hasRailInFront) // in case the player Cart got stucked between 2 rails
            {
                //Debug.Log("Freeze error due to nextPos = worldPos: at " + worldPos);

                // Unstuck by moving the player Cart forward into the next rail
                transform.Translate(transform.forward * Time.deltaTime * playerCartMovement._PlayerCartSpeed, Space.World);
            }
            else
            {
                //Setting the player's position and adding a height offset so that they're sitting on top of the rail instead of being in the middle of it.
                transform.position = worldPos + (transform.up * heightOffset);
                //Lerping the player's current rotation to the direction of where they are to where they're going.
                Vector3 lookRota = nextPos - worldPos;
                if (playerStatusController.playerCurrentStatus != PlayerStatus.Jump)
                {
                    // 1. Get player look direction (forward along the rail)
                    Quaternion lookRot = Quaternion.LookRotation(lookRota.normalized, up);

                    // 2. Smoothly blend player rotation
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, lerpSpeed * Time.deltaTime);

                    // 3. Apply tilt input for cart
                    Quaternion tilted = playerCartMovement.GetTiltControlRotation(lookRot);

                    // 4. Apply tilt input for cart
                    cartTranform.rotation = Quaternion.Slerp(cartTranform.rotation, tilted, lerpSpeed * Time.deltaTime);
                }
            }

            //Finally incrementing or decrementing elapsed time for the next update based on direction.
            if (currentRailScript.normalDir)
                elapsedTime += Time.deltaTime;
            else
                elapsedTime -= Time.deltaTime;
        }
    }



    public void OnRailDetectEnter(Collider other)
    {
        if (other.gameObject.tag == "Rail")
        {
            playerStatusController.playerCurrentStatus = PlayerStatus.OnRail;
            gravitySim.isFalling = false;
            playerCartMovement.StopJumpingCoroutines();
            /*When the player hits the rail, onRail is set to true, the current rail script is set to the
             *rail script of the rail the player hits. Then we calculate the player's position on that rail.*/
            onRail = true;
            currentRailScript = other.gameObject.GetComponent<RailScript>();
            //if (currentRailScript != null)
            //{
            //    Debug.Log("Enter " + other.transform.parent.name + " rail!");
            //}
            //else
            //{
            //    Debug.Log("Unable to enter " + other.transform.parent.name + " rail!");
            //}
            CalculateAndSetRailPosition();
        }
    }

    public void OnRailDetectExit(Collider railCollider, bool _hasRailInRange)
    {
        if (railCollider.gameObject.tag == "Rail")
        {
            //Debug.Log("Exit rail: " + railCollider.transform.parent.name);
            gravitySim.isFalling = true;
            if (currentRailScript != railCollider.gameObject.GetComponent<RailScript>() && currentRailScript != null)
            {
                return;
            }
            if (playerStatusController.playerCurrentStatus == PlayerStatus.OnRail)
            {
                playerStatusController.playerCurrentStatus = PlayerStatus.OffRail;
            }
            if (!_hasRailInRange && !RailDetector.Instance._hasRailInRange)
            {
                DeadEndJumpOffCliff();
            }
        }
    }

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

        //Spline evaluate takes the 0 to 1 normalised time above, and uses it to give you a local position, a tangent (forward), and up
        float3 pos, forward, up;
        SplineUtility.Evaluate(currentRailScript.railSpline.Spline, normalisedTime, out pos, out forward, out up);

        // Convert forward vector from local to world space
        Vector3 worldForward = currentRailScript.transform.TransformDirection(forward);

        //Calculate the direction the player is going down the rail
        currentRailScript.CalculateDirection(worldForward, transform.forward);

        //Set player's initial position on the rail before starting the movement code.
        transform.position = splinePoint + (transform.up * heightOffset);
    }
    void ThrowOffRail()
    {
        //Set onRail to false, clear the rail script, and push the player off the rail.
        //It's a little sudden, there might be a better way of doing using coroutines and looping, but this will work.
        onRail = false;
        currentRailScript = null;
        if (playerStatusController.playerCurrentStatus != PlayerStatus.Jump)
        {
            playerStatusController.playerCurrentStatus = PlayerStatus.OffRail;
            transform.position += transform.forward * 1f;
            //playerRigidbody.useGravity = true;
            gravitySim.isFalling = true;
        }
        Debug.Log("Thrown off rail");
    }

    public void EmptyCurrentRailScript()
    {
        currentRailScript = null;
    }

    public bool IsCurrentRailScriptEmpty()
    {
        if (currentRailScript == null)
            return true;
        else
            return false;
    }

    public void DeadEndJumpOffCliff()
    {
        ThrowOffRail();
        playerCartMovement._movePhysic = true;
    }


}
