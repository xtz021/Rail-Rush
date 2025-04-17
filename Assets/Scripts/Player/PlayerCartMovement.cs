using System.Collections;
using System.Collections.Generic;
using Unity.Splines.Examples;
using UnityEngine;

public class PlayerCartMovement : MonoBehaviour
{
    // Attributes for jump calculations
    public float jumpHeight = 15f;
    public float jumpOnAirDuration = 0.75f;
    public float distantBetweenRails = 2.75f;
    public float _PlayerCartSpeed = 8f;

    public bool _movePhysic = false;


    private float normalX;
    private float normalY;
    private float maxTiltAngle = 45f;
    private float tiltMagnitude = 20f;
    private float tiltSpeed = 20f;
    private Rigidbody playerRigidBody;
    private PlayerStatusController playerStatusController;
    private PlayerCartGrindMovement playerCartGrindMovement;
    private CharacterAnimationController characterAnimationController;

    [SerializeField] CartAnimationController cartAnimationController;


    // Vectors for swipe calculations
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private bool touchControlOnCooldown = false;
    private int tiltDirection = 0;
    private Coroutine jumpCoroutine;
    private Coroutine touchCooldownCoroutine;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerStatusController = GetComponent<PlayerStatusController>();
        playerCartGrindMovement = GetComponent<PlayerCartGrindMovement>();
        characterAnimationController = transform.Find("PlayerCharacter").GetComponent<CharacterAnimationController>();
    }

    private void Update()
    {
        if (playerStatusController.playerCurrentStatus != PlayerStatus.Dead)
        {
            if (playerStatusController.playerCurrentStatus == PlayerStatus.OffRail && _movePhysic)
            {
                playerRigidBody.useGravity = true;
            }
            TouchControl();
        }
        else
        {
            // when player is dead
        }
    }


    private void PremNormalPos()
    {
        normalX = transform.position.x;
        normalY = transform.position.y;
    }


    public void TouchControl()
    {
        if(!touchControlOnCooldown)
        {
            if (Input.touches.Length > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    //save began touch 2d point
                    firstPressPos = new Vector2(t.position.x, t.position.y);
                }
                if (t.phase == TouchPhase.Ended)
                {
                    //save ended touch 2d point
                    secondPressPos = new Vector2(t.position.x, t.position.y);

                    //create vector from the two points
                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                    //normalize the 2d vector
                    currentSwipe.Normalize();

                    //swipe upwards
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        PremNormalPos();
                        Jump(0);
                        characterAnimationController.JumpCenter();
                        //JumpForce();
                        Debug.Log("up swipe");
                    }
                    //swipe down
                    if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        characterAnimationController.Crouch();                                  // Play Crouch animation
                        Debug.Log("down swipe");
                    }
                    //swipe left
                    if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        if (playerStatusController.playerCurrentRail > -1)
                        {
                            PremNormalPos();
                            Jump(-1);
                            characterAnimationController.JumpLeft();
                            playerStatusController.playerCurrentRail--;
                        }
                        Debug.Log("left swipe");
                    }
                    //swipe right
                    if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        if (playerStatusController.playerCurrentRail < 1)
                        {
                            PremNormalPos();
                            Jump(1);
                            characterAnimationController.JumpRight();
                            playerStatusController.playerCurrentRail++;
                        }
                        Debug.Log("right swipe");
                    }
                }
            }
            TiltControlSimulatorForEditor();        // for Editor only
            //TiltCartControl();
        }
    }

    private void TiltCartControl()
    {
        Vector3 tilt = Input.acceleration;
        Vector3 targetPos = new Vector3(0,0,0);
        if(tilt.x >= 0.25f)
        {
            targetPos.z = -maxTiltAngle;
        }
        else if(tilt.x <= -0.25)
        {
            targetPos.z = maxTiltAngle;
        }
        Quaternion targetRotation = Quaternion.Euler(targetPos);
        //float tiltX = Mathf.Clamp(tilt.x * tiltMagnitude, -maxTiltAngle,maxTiltAngle);      // Get tilt on X rotation but limit it in (-45;45) angle
        //transform.rotation = Quaternion.Euler(0,0,-tiltX);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }
    private void TiltControlSimulatorForEditor()                // For testing in editor only
    {
        //Vector3 targetPos = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.D))
        {
            //targetPos.z = -maxTiltAngle;
            tiltDirection = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //targetPos.z = maxTiltAngle;
            tiltDirection = -1;
        }
        else
        {
            tiltDirection = 0;
        }
        //Quaternion targetRotation = Quaternion.Euler(targetPos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }

    public Quaternion GetTiltControlRotation(Quaternion baseRotation)           // To use in PlayerCartGrindiMovement while grinding on Spline
    {
        //Vector3 targetPos = new Vector3(0, 0, 0);
        //if (tiltDirection < 0)
        //{
        //    targetPos.z = maxTiltAngle;
        //}
        //else if(tiltDirection > 0)
        //{
        //    targetPos.z = -maxTiltAngle;
        //}
        // Determine desired tilt angle (roll)
        float tiltAngle = 0f;
        if (tiltDirection < 0)
            tiltAngle = maxTiltAngle;
        else if (tiltDirection > 0)
            tiltAngle = -maxTiltAngle;
        //Quaternion targetRotation = Quaternion.Euler(targetPos);
        //Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
        //return rotation;
        // Apply tilt (roll) around the local forward axis
        Vector3 forwardAxis = baseRotation * Vector3.forward;
        Quaternion tiltRotation = Quaternion.AngleAxis(tiltAngle, forwardAxis);

        // Apply tilt to base rotation
        Quaternion targetRotation = tiltRotation * baseRotation;

        // Smoothly blend toward target rotation
        return Quaternion.Lerp(baseRotation, targetRotation, tiltSpeed * Time.deltaTime);
    }


    private void Jump(int jumpDirection)
    {
        //Debug.Log("Before jump: " + transform.rotation.eulerAngles);
        jumpCoroutine = StartCoroutine(JumpIE2(jumpDirection));
        touchCooldownCoroutine = StartCoroutine(TouchControlGoesOnCooldown());
        cartAnimationController.JumpAnimation(jumpDirection);                   // Playing jump animation
        playerCartGrindMovement.EmptyCurrentRailScript();
    }

    IEnumerator JumpIE(int jumpDirection)       //jumpDirection = 0 -> jump straight up
                                                //jumpDirection = 1 -> jump right
                                                //jumpDirection = -1 -> jump left
    {
        float timer = 0f;
        float jumpOnAirDuration = 0.5f;
        playerCartGrindMovement.onRail = false;
        jumpDirection = NormalizedIntDirection(jumpDirection);
        playerStatusController.playerCurrentStatus = PlayerStatus.Jump;
        while (timer < jumpOnAirDuration)
        {
            transform.position = new Vector3(normalX + (timer / jumpOnAirDuration) * distantBetweenRails * jumpDirection
                                                , normalY + _PlayerCartSpeed * Time.deltaTime + Mathf.Sin(timer / jumpOnAirDuration * Mathf.PI) * jumpHeight
                                                , transform.position.z + _PlayerCartSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0,0,1)),5*Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, normalY, transform.position.z);
        playerStatusController.playerCurrentStatus = PlayerStatus.OffRail;
        playerRigidBody.useGravity = true;
        yield break;
    }

    IEnumerator JumpIE2(int jumpDirection)
    {
        float timer = 0f;
        float jumpOnAirDuration = 0.5f;
        playerCartGrindMovement.onRail = false;
        jumpDirection = NormalizedIntDirection(jumpDirection);
        playerStatusController.playerCurrentStatus = PlayerStatus.Jump;

        // Capture the starting state ONCE
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        // Calculate the jump direction based on initial orientation only
        Vector3 jumpOffset = startRotation * Vector3.right * jumpDirection * distantBetweenRails;
        Vector3 forwardOffset = startRotation * Vector3.forward * _PlayerCartSpeed * jumpOnAirDuration;

        while (timer < jumpOnAirDuration)
        {
            float t = timer / jumpOnAirDuration;

            Vector3 basePosition = startPosition + jumpOffset * t + forwardOffset * t;
            float vertical = Mathf.Sin(t * Mathf.PI) * jumpHeight;

            transform.position = new Vector3(basePosition.x, normalY + vertical, basePosition.z);

            // Freeze rotation — no re-interpolation!
            transform.rotation = startRotation;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, normalY, transform.position.z);
        transform.rotation = startRotation;
        if (playerStatusController.playerCurrentStatus != PlayerStatus.OnRail)
        {
            playerStatusController.playerCurrentStatus = PlayerStatus.OffRail;
            playerRigidBody.useGravity = true;
        }
        yield break;
    }

    IEnumerator TouchControlGoesOnCooldown()
    {
        touchControlOnCooldown = true;
        yield return new WaitForSeconds(jumpOnAirDuration + 0.1f);
        touchControlOnCooldown = false;
        yield break;
    }

    public void StopJumpingCoroutines()
    {
        if(jumpCoroutine != null)
        {
            //StopAllCoroutines();
            StopCoroutine(jumpCoroutine);
        }
    }


    private int NormalizedIntDirection(int direction)
    {
        if(direction > 0)
        {
            direction = 1;
        }
        else if(direction < 0) 
        {
            direction = -1;
        }
        return direction;
    }

}
