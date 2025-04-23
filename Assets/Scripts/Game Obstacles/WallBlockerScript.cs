using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlockerScript : MonoBehaviour
{
    private const string STRING_OBSTACLE_TYPE = "Wall";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Obstacle hits the player!");
            PlayerStatusController playerStatusController = collision.gameObject.GetComponent<PlayerStatusController>();
            if (playerStatusController == null)             // When the wall hits the character instead of the cart
            {
                playerStatusController = collision.transform.parent.parent.GetComponent<PlayerStatusController>();
                Debug.Log("Obstacle hits the character!");
            }
            playerStatusController.Die(STRING_OBSTACLE_TYPE);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Obstacle hits the character!");
            PlayerStatusController playerStatusController;
            playerStatusController = GetStatusControllerFromHead(other.transform);  // Get PlayerStatusController from the player
            if (playerStatusController != null && playerStatusController.playerCurrentStatus != PlayerStatus.Dead)
            {
                playerStatusController.Die(STRING_OBSTACLE_TYPE);
            }
            else
            {
                //Debug.Log($"Incorrect transform: {playerStatusController.transform.name}");
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
            Debug.Log($"Incorrect transform: {player.name}");
        }
        return playerStatusController;
    }
}
