using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public static PlayerStatusController Instance { get; private set; }

    public PlayerStatus playerCurrentStatus;
    public int playerCurrentRail;
    public bool canJumpLeft = true;
    public bool canJumpRight = true;

    //private Rigidbody playerRigidbody;
    private Collider playerCollider;
    private CartAnimationController cartAnim;
    private CharacterAnimationController characterAnim;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple instances of PlayerStatusController detected. Destroying duplicate.");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        playerCurrentStatus = PlayerStatus.OffRail;
        playerCurrentRail = 0;
        //playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        cartAnim = CartAnimationController.Instance;
        characterAnim = CharacterAnimationController.Instance;
    }

    public void Die(string obstacleType)
    {
        if(playerCurrentStatus != PlayerStatus.Dead)
        {
            playerCurrentStatus = PlayerStatus.Dead;
            Debug.Log($"Player is dead by {obstacleType}");
            //playerRigidbody.useGravity = false;
            cartAnim.DeadAnimation(obstacleType);
            characterAnim.Dead(obstacleType);
        }
    }


}


public enum PlayerStatus
{
    Jump, OnRail, OffRail, Dead
}
