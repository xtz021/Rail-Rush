using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CaveSpawner : MonoBehaviour
{
    List<GameObject> cavesList_TurnLeft;
    List<GameObject> cavesList_Straight;
    List<GameObject> cavesList_TurnRight;

    void Start()
    {
        cavesList_Straight = transform.parent.GetComponent<CavesListController>().GetCaveListStraight();
        cavesList_TurnLeft = transform.parent.GetComponent<CavesListController>().GetCaveListLeft();
        cavesList_TurnRight = transform.parent.GetComponent<CavesListController>().GetCaveListRight();
        SpawnRandomCave();
    }


    private void SpawnRandomCave()
    {
        string railName = transform.name;
        if (CaveType(railName) == 1)
        {
            SpawnCaveTurnRight(transform);
        }
        else if (CaveType(railName) == -1)
        {
            SpawnCaveTurnLeft(transform);
        }
        else
        {
            SpawnCaveStraight(transform);
        }
    }

    private void SpawnCaveStraight(Transform spawnPoint)
    {
        Vector3 spawnPos = spawnPoint.position;
        Quaternion spawnRota = transform.rotation;
        GameObject cavePref;
        cavePref = cavesList_Straight[Random.Range(0, cavesList_Straight.Count)];
        GameObject caveSpawn = Instantiate<GameObject>(cavePref, spawnPos, spawnRota, transform);
        Debug.Log("Spawn cave straight");
    }

    private void SpawnCaveTurnLeft(Transform spawnPoint)
    {
        Vector3 spawnPos = spawnPoint.position;
        Quaternion spawnRota = transform.rotation;               
        GameObject cavePref;
        cavePref = cavesList_TurnLeft[Random.Range(0, cavesList_TurnLeft.Count)];
        GameObject caveSpawn = Instantiate<GameObject>(cavePref, spawnPos, spawnRota, transform);
        Debug.Log("Spawn cave turn left");
    }

    private void SpawnCaveTurnRight(Transform spawnPoint)
    {
        Vector3 spawnPos = spawnPoint.position;
        Quaternion spawnRota = transform.rotation;               
        GameObject cavePref;
        cavePref = cavesList_TurnRight[Random.Range(0, cavesList_TurnRight.Count)];
        GameObject caveSpawn = Instantiate<GameObject>(cavePref, spawnPos, spawnRota, transform);
        Debug.Log("Spawn cave turn right");
    }

    private int CaveType(string railName)                              // return 0  -> cave straight
                                                        // return 1  -> cave turn right 45
                                                        // return -1 -> cave turn left 45
    {
        Debug.Log($"Rail name: {railName}");
        if(railName.Contains("CurveLeft45"))
        {
            return -1;
        }
        else if(railName.Contains("CurveRight45"))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

}
