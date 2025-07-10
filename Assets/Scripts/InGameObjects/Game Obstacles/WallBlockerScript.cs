using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallBlockerScript : MonoBehaviour
{
    private const string STRING_OBSTACLE_TYPE = "Wall";
    [SerializeField] private ObstacleType obstacleType = ObstacleType.Wall;
    private Animation obsAnimation;
    private bool isDestroyed = false;

    private void Awake()
    {
        obsAnimation = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isDestroyed)
        {
            Debug.Log($"Obstacle hits the character! Gameobject: {other.name}");
            PlayerStatusController playerStatusController = PlayerStatusController.Instance;
            if (playerStatusController != null && playerStatusController.playerCurrentStatus != PlayerStatus.Dead)
            {
                Debug.Log($"Player {playerStatusController.transform.name} is dead by {STRING_OBSTACLE_TYPE} obstacle");
                playerStatusController.Die(STRING_OBSTACLE_TYPE);
                UpdatePlayerStats(); // Update player stats info
                obsAnimation.Play();
                if(obstacleType == ObstacleType.Wall)
                {
                    AudioManager.Instance.Play("PanelBreak");
                }
                else
                {
                    AudioManager.Instance.Play("WoodCrash");
                }
                isDestroyed = true;
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
        if(obstacleType == ObstacleType.Upper)
        {
            GameStatsController.Instance.playerStats.DeathsByUpperObs++;
        }
        else if(obstacleType == ObstacleType.Lower)
        {
            GameStatsController.Instance.playerStats.DeathsByLowerObs++;
        }
        else if(obstacleType == ObstacleType.Left)
        {
            GameStatsController.Instance.playerStats.DeathsByLeftObs++; 
        }
        else if(obstacleType == ObstacleType.Right)
        {
            GameStatsController.Instance.playerStats.DeathsByRightObs++;
        }
    }

    private void DestroyAfterAnimation()
    {
        Debug.Log($"Destroying {gameObject.name} after animation");
        Destroy(gameObject);
    }

    public ObstacleType GetObstacleType()
    {
        return obstacleType;
    }

    public void DestroyedByPlayer()
    {
        isDestroyed = true;
        obsAnimation.Play();
        Debug.Log($"Obstacle {gameObject.name} is set as destroyed");
    }
}

[System.Serializable]
public enum ObstacleType
{
    Wall, Upper, Lower, Left, Right
}
