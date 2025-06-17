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
            PlayerStatusController playerStatusController = PlayerStatusController.Instance;
            if (playerStatusController != null && playerStatusController.playerCurrentStatus != PlayerStatus.Dead)
            {
                Debug.Log($"Player {playerStatusController.transform.name} is dead by {STRING_OBSTACLE_TYPE} obstacle");
                playerStatusController.Die(STRING_OBSTACLE_TYPE);
                UpdatePlayerStats(); // Update player stats info
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

    private void UpdatePlayerStats()
    {
        if(gameObject.name.Contains("Upper"))
        {
            GameStatsController.Instance.playerStats.DeathsByUpperObs++;
        }
        else if(gameObject.name.Contains("Lower"))
        {
            GameStatsController.Instance.playerStats.DeathsByLowerObs++;
        }
        else if(gameObject.name.Contains("Left"))
        {
            GameStatsController.Instance.playerStats.DeathsByLeftObs++; 
        }
        else if (gameObject.name.Contains("Right"))
        {
            GameStatsController.Instance.playerStats.DeathsByRightObs++;
        }
    }

    private void DestroyAfterAnimation()
    {
        Debug.Log($"Destroying {gameObject.name} after animation");
        Destroy(gameObject);
    }
}
