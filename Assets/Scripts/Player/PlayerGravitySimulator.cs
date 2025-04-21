using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravitySimulator : MonoBehaviour
{
    public float verticalVelocity = 0f; // Current vertical speed
    public bool isFalling;

    private PlayerCartGrindMovement playerCartGrindMovement;
    private PlayerStatusController playerStatusController;
    private const float gravity = -9.81f;

    private void Awake()
    {
        isFalling = true;
        playerCartGrindMovement = GetComponent<PlayerCartGrindMovement>();
        playerStatusController = GetComponent<PlayerStatusController>();
    }

    private void FixedUpdate()
    {
        if (isFalling && playerCartGrindMovement.IsCurrentRailScriptEmpty() && playerStatusController.playerCurrentStatus != PlayerStatus.Dead)
        {
            verticalVelocity = gravity;
            //transform.position += new Vector3(0, verticalVelocity * Time.deltaTime, 0);
            transform.Translate(new Vector3 (0, verticalVelocity * Time.fixedDeltaTime, 0), Space.Self);
            //transform.position += new Vector3(0, verticalVelocity * Time.fixedDeltaTime, 0);

            if (transform.position.y <= -5f)
            {
                isFalling = false;
                Debug.Log("Player fall off cliff");
                verticalVelocity = 0f;
                playerStatusController.playerCurrentStatus = PlayerStatus.Dead;
            }
        }
        else
        {
            verticalVelocity = 0;
        }
    }


}
