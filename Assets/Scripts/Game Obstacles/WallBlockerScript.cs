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
}
