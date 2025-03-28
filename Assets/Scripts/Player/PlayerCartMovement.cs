using System.Collections;
using System.Collections.Generic;
using Unity.Splines.Examples;
using UnityEngine;

public class PlayerCartMovement : MonoBehaviour
{
    // Attributes for jump calculations
    public float jumpForce = 15f;
    public float jumpHeight = 5f;
    private float normalY;
    private Rigidbody playerRigidBody;
    private PlayerStatusController playerStatusController;

    [SerializeField] Transform playerCart;

    private float _PlayerCartSpeed = 15f;

    // Vectors for swipe calculations
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerStatusController = GetComponent<PlayerStatusController>();
    }

    private void Update()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * _PlayerCartSpeed, Space.World);
        TouchControl();
    }


    public void TouchControl()
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
                    StartCoroutine(JumpIE());
                    //JumpForce();
                    Debug.Log("up swipe");
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
		        {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
		        {
                    Debug.Log("left swipe");
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
		        {
                    Debug.Log("right swipe");
                }
            }
        }
    }

    IEnumerator JumpIE()
    {
        float timer = 0f;
        float duration = 1f;

        while (timer < duration)
        {
            playerStatusController.playerCurrentStatus = PlayerStatus.Jump;
            playerCart.position = new Vector3(playerCart.position.x, normalY + Mathf.Sin(timer / duration * Mathf.PI) * jumpHeight, playerCart.position.z);
            timer += Time.deltaTime;
            yield return null;
        }
        playerCart.position = new Vector3(playerCart.position.x, normalY, playerCart.position.z);
        playerStatusController.playerCurrentStatus = PlayerStatus.OffRail;
    }

    private void JumpForce()
    {
        if(playerStatusController.playerCurrentStatus == PlayerStatus.OnRail)
        {
            playerRigidBody.useGravity = true;
            Vector3 offSetBeforeJump = new Vector3(0f, 0.5f, 0f);
            //transform.position += offSetBeforeJump;
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerStatusController.playerCurrentStatus = PlayerStatus.Jump;
        }
    }

}
