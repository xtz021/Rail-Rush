using System.Collections;
using System.Collections.Generic;
using Unity.Splines.Examples;
using UnityEngine;

public class PlayerCartMovement : MonoBehaviour
{
    // Attributes for jump calculations
    public float jumpForce = 15f;
    public float jumpHeight = 5f;
    public float jumpOnAirDuration = 0.5f;
    public float distantBetweenRails = 2.75f;
    public float _PlayerCartSpeed = 10f;

    public bool _movePhysic = false;


    private float normalX;
    private float normalY;
    private float maxTiltAngle = 45f;
    private float tiltSpeed = 5f;
    private Rigidbody playerRigidBody;
    private PlayerStatusController playerStatusController;
    private PlayerCartGrindMovement playerCartGrindMovement;
    private CharacterAnimationController characterAnimationController;

    [SerializeField] Transform playerCart;
    [SerializeField] CartAnimationController cartAnimationController;


    // Vectors for swipe calculations
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private bool touchControlOnCooldown = false;
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
        if(playerStatusController.playerCurrentStatus == PlayerStatus.OffRail && _movePhysic)
        {
            playerRigidBody.useGravity = true;
            //MoveForward();
        }
        TouchControl();
    }

    public void MoveForward()
    {
        if(playerRigidBody.useGravity == false)
        {
            //playerRigidBody.useGravity = true;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * _PlayerCartSpeed, Space.World);
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
                            playerStatusController.playerCurrentRail++;
                        }
                        Debug.Log("right swipe");
                    }
                }
            }
            TiltCartControl();
        }
    }

    private void TiltCartControl()
    {
        Vector3 tilt = Input.acceleration;
        float tiltX = Mathf.Clamp(tilt.x * tiltSpeed, -maxTiltAngle,maxTiltAngle);      // Get tilt on X rotation but limit it in (-45;45) angle
        transform.rotation = Quaternion.Euler(tiltX,0,0);
    }

    private void Jump(int jumpDirection)
    {
        jumpCoroutine = StartCoroutine(JumpIE(jumpDirection));
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
            playerCart.position = new Vector3(normalX + (timer / jumpOnAirDuration) * distantBetweenRails * jumpDirection
                                                , normalY + _PlayerCartSpeed * Time.deltaTime + Mathf.Sin(timer / jumpOnAirDuration * Mathf.PI) * jumpHeight
                                                , playerCart.position.z + _PlayerCartSpeed * Time.deltaTime);
            playerCart.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0,0,1)),5*Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        playerCart.position = new Vector3(playerCart.position.x, normalY, playerCart.position.z);
        //Debug.Log("Landed");
        playerStatusController.playerCurrentStatus = PlayerStatus.OffRail;
    }

    IEnumerator TouchControlGoesOnCooldown()
    {
        touchControlOnCooldown = true;
        yield return new WaitForSeconds(jumpOnAirDuration);
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

    private void JumpForce()                // Not using this at the moment
    {
        if (playerStatusController.playerCurrentStatus == PlayerStatus.OnRail)
        {

        }
        playerRigidBody.useGravity = true;
        Vector3 offSetBeforeJump = new Vector3(0f, 0.5f, 0f);
        //transform.position += offSetBeforeJump;
        playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerStatusController.playerCurrentStatus = PlayerStatus.Jump;
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
