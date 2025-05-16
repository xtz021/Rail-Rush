using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravitySimulator : MonoBehaviour
{
    public static PlayerGravitySimulator Instance { get; private set; }
    public float verticalVelocity = 0f; // Current vertical speed
    public bool isFalling;

    private PlayerCartGrindMovement playerCartGrindMovement;
    private PlayerStatusController playerStatusController;
    private const float gravity = -9.81f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        isFalling = true;
    }
    private void Start()
    {
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

            //if (transform.position.y <= -5f)
            //{
            //    isFalling = false;
            //    Debug.Log("Player falls off cliff");
            //    verticalVelocity = 0f;
            //    playerStatusController.playerCurrentStatus = PlayerStatus.Dead;
            //}
        }
        else
        {
            verticalVelocity = 0;
        }
    }


}
