using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CaveSpawner : MonoBehaviour
{
    List<GameObject> cavesList_TurnLeft;
    List<GameObject> cavesList_Straight_Big;
    List<GameObject> cavesList_Straight_Med;
    List<GameObject> cavesList_Straight_Small;
    List<GameObject> cavesList_TurnRight;

    void Start()
    {
        cavesList_Straight_Big = transform.parent.GetComponent<CavesListController>().GetCaveListStraight_Big();
        cavesList_Straight_Med = transform.parent.GetComponent<CavesListController>().GetCaveListStraight_Med();
        cavesList_Straight_Small = transform.parent.GetComponent<CavesListController>().GetCaveListStraight_Small();
        cavesList_TurnLeft = transform.parent.GetComponent<CavesListController>().GetCaveListLeft();
        cavesList_TurnRight = transform.parent.GetComponent<CavesListController>().GetCaveListRight();
        SpawnRandomCave();
    }


    private void SpawnRandomCave()
    {
        string railName = transform.name;
        if (CaveType(railName) == 1)
        {
            //SpawnCaveTurnRight(transform);
            SpawnCave(transform, cavesList_TurnRight);
        }
        else if (CaveType(railName) == -1)
        {
            //SpawnCaveTurnLeft(transform);
            SpawnCave(transform, cavesList_TurnLeft);
        }
        else
        {
            // Check cave size
            Transform endPoint = transform.Find("EndPoint");
            float railLength = endPoint.localPosition.z;
            if (railLength >= 20)
            {
                SpawnCave(transform,cavesList_Straight_Big);
            }
            else if(railLength < 20 && railLength >= 12)
            {
                SpawnCave(transform, cavesList_Straight_Med);
            }
            else
            {
                SpawnCave(transform,cavesList_Straight_Small);
            }
        }
    }

    private void SpawnCave(Transform spawnPoint, List<GameObject> cavesList)
    {
        Vector3 spawnPos = spawnPoint.position;
        Quaternion spawnRota = transform.rotation;
        GameObject cavePref;
        cavePref = cavesList[Random.Range(0, cavesList.Count)];
        GameObject caveSpawn = Instantiate<GameObject>(cavePref, spawnPos, spawnRota, transform);
        Debug.Log($"Spawn cave {cavePref.name}");
    }

    private int CaveType(string railName)                               // return 0  -> cave straight
                                                                        // return 1  -> cave turn right 45
                                                                        // return -1 -> cave turn left 45
    {
        //Debug.Log($"Rail name: {railName}");
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
