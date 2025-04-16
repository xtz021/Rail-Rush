using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public PlayerStatus playerCurrentStatus;
    public int playerCurrentRail;

    private Rigidbody playerRigidbody;
    private Collider playerCollider;
    private CartAnimationController cartAnim;
    private CharacterAnimationController characterAnim;

    private void Start()
    {
        playerCurrentStatus = PlayerStatus.OffRail;
        playerCurrentRail = 0;
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        cartAnim = transform.Find("MiningCart").GetComponent<CartAnimationController>();
        characterAnim = transform.Find("PlayerCharacter").GetComponent<CharacterAnimationController>();
    }

    public void Die(string obstacleType)
    {
        playerCurrentStatus = PlayerStatus.Dead;
        Debug.Log($"Player is dead by {obstacleType}");
        playerRigidbody.useGravity = false;
        cartAnim.DeadAnimation(obstacleType);
        characterAnim.Dead(obstacleType);
    }


}


public enum PlayerStatus
{
    Jump, OnRail, OffRail, Dead
}
