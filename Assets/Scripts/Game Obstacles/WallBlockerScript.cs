using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallBlockerScript : MonoBehaviour
{
    private const string STRING_OBSTACLE_TYPE = "Wall";
    private Animation obsAnimation;

    private void Awake()
    {
        obsAnimation = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log($"Obstacle hits the character! Gameobject: {other.name}");
            PlayerStatusController playerStatusController;
            if(other.name.Contains("head"))  // Get PlayerStatusController from the player
            {
                playerStatusController = GetStatusControllerFromHead(other.transform);
            }
            else if(other.name.Contains("Cart"))
            {
                playerStatusController = GetStatusControllerFromCart(other.transform);
            }
            else
            {
                Debug.Log("Not cart or head");
                return;
            }
            if (playerStatusController != null && playerStatusController.playerCurrentStatus != PlayerStatus.Dead)
            {
                Debug.Log($"Player {playerStatusController.transform.name} is dead by {STRING_OBSTACLE_TYPE} obstacle");
                playerStatusController.Die(STRING_OBSTACLE_TYPE);
                obsAnimation.Play();
            } else if (playerStatusController == null)
            {
                Debug.Log($"PlayerStatusController is null for {other.name}");
            }
            else
            {
                Debug.Log($"Incorrect transform: {playerStatusController.transform.name}");
            }
        }
    }

    private PlayerStatusController GetStatusControllerFromHead(Transform head)
    {
        Transform player = head;
        for (int i = 0; i < 9; i++)                             // Get Player transform
        {
            player = player.parent;
        }
        PlayerStatusController playerStatusController = player.GetComponent<PlayerStatusController>();
        if (playerStatusController == null)
        {
            Debug.Log($"Incorrect transform from head: {player.name}");
        }
        return playerStatusController;
    }

    private PlayerStatusController GetStatusControllerFromCart(Transform cart)
    {
        Transform player = cart.parent;             // Get Player transform
        PlayerStatusController playerStatusController = player.GetComponent<PlayerStatusController>();
        if (playerStatusController == null)
        {
            Debug.Log($"Incorrect transform from cart: {player.name}");
        }
        return playerStatusController;
    }

    private void DestroyAfterAnimation()
    {
        Debug.Log($"Destroying {gameObject.name} after animation");
        Destroy(gameObject);
    }
}
