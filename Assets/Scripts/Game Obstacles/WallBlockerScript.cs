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
            PlayerStatusController playerStatusController = collision.gameObject.GetComponent<PlayerStatusController>();
            playerStatusController.Die(STRING_OBSTACLE_TYPE);
        }
    }
}
