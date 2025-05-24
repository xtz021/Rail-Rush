using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private GameObject wall_Obstacle;
    private GameObject left_Obstacle;
    private GameObject right_Obstacle;
    private GameObject up_Obstacle;
    private GameObject down_Obstacle;

    private const string STRING_TAG_OBSTACLESPAWNPOINT = "ObsSpawnPoint";
    private List<GameObject> obstacleSpawnPointsList;

    void Start()
    {
        wall_Obstacle = ObstacleTypesController.Instance.wall_Obstacle;
        left_Obstacle = ObstacleTypesController.Instance.left_Obstacle;
        right_Obstacle = ObstacleTypesController.Instance.right_Obstacle;
        up_Obstacle = ObstacleTypesController.Instance.up_Obstacle;
        down_Obstacle = ObstacleTypesController.Instance.down_Obstacle;
        CheckAndSpawnObstacle();
    }

    private void SpawnObstacle(GameObject obstaclePrefab, Transform spawnPoint)
    {
        Instantiate(obstaclePrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint.parent);
    }

    private void CheckAndSpawnObstacle()
    {
        obstacleSpawnPointsList = CheckAndGetObstacleList();
        foreach (GameObject spawnPoint in obstacleSpawnPointsList)
        {
            if(Random.Range(0, 2) == 0) // Randomly decide to spawn obstacle or not
            {
                if (spawnPoint.name.Contains("Soft"))
                {
                    int type = Random.Range(1, 4); // Randomly choose obstacle type
                    switch(type)
                    {
                        case 1:
                            SpawnObstacle(left_Obstacle, spawnPoint.transform);
                            break;
                        case 2:
                            SpawnObstacle(right_Obstacle, spawnPoint.transform);
                            break;
                        case 3:
                            SpawnObstacle(up_Obstacle, spawnPoint.transform);
                            break;
                        case 4:
                            SpawnObstacle(down_Obstacle, spawnPoint.transform);
                            break;
                        default:
                            SpawnObstacle(wall_Obstacle, spawnPoint.transform);
                            break;
                    }
                }
                else
                {
                    SpawnObstacle(wall_Obstacle, spawnPoint.transform);
                }
            }
        }
    }

    private List<GameObject> CheckAndGetObstacleList()
    {
        List<GameObject> ObsList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.tag == STRING_TAG_OBSTACLESPAWNPOINT)
            {
                ObsList.Add(child.gameObject);
            }
        }
        return ObsList;
    }
}
