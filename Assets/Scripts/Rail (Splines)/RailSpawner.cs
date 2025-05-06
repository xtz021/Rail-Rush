using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RailSpawner : MonoBehaviour
{
    List<GameObject> railsList_Left;
    List<GameObject> railsList_Center;
    List<GameObject> railsList_Right;

    bool hasSpawn;
    RailsListController railsListController;

    private void Start()
    {
        hasSpawn = false;
        railsListController = transform.parent.GetComponent<RailsListController>();
        railsList_Center = railsListController.GetRailListStraight();
        railsList_Left = railsListController.GetRailListLeft();
        railsList_Right = railsListController.GetRailListRight();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !hasSpawn)
        {
            SpawnRandomRail();
        }
    }

    private void SpawnRandomRail()
    {
        Transform latestRail = railsListController.GetLastRailInQueue().transform;
        Transform nextSpawnPoint = latestRail.Find("NextLink0");
        List<Transform> endPoints = new List<Transform>();          // Points where the rail ends, can end with multiple rail on left, right and center
        foreach (Transform child in latestRail)
        {
            if (child.tag == "EndPoint")
            {
                endPoints.Add(child);
            }
        }
        if (endPoints.Count >= 1)
        {
            int endPointIndex = Random.Range(0, endPoints.Count);   // Get end point to spawn using an suitable rail list
            if (endPoints[endPointIndex].localPosition.x == nextSpawnPoint.localPosition.x)
            {
                SpawnRailCenter(nextSpawnPoint);
            }
            else if (endPoints[endPointIndex].localPosition.x > nextSpawnPoint.localPosition.x)
            {
                SpawnRailRight(nextSpawnPoint);
            }
            else
            {
                SpawnRailLeft(nextSpawnPoint);
            }
        }
        else
        {
            Debug.Log("Unable to get rail End Point");
        }
    }


    private void SpawnRailCenter(Transform spawnPoint)
    {
        Vector3 spawnPos = spawnPoint.position;
        Quaternion spawnRota = new Quaternion();
        spawnRota.eulerAngles = spawnPoint.localEulerAngles + spawnPoint.parent.eulerAngles;  // rotation = this rail rotation + NextLink0 rotation
        GameObject railPref;
        //do
        {
            railPref = railsList_Center[Random.Range(0, railsList_Center.Count)];
        } //while (railPref == PrefabUtility.GetCorrespondingObjectFromSource(gameObject) && railPref != null);   // check if the newly spawn rail is the same with this one
                                                                                                                  // Currently not working properly
        //GameObject railSpawn = Instantiate<GameObject>(railPref, spawnPos, spawnRota, transform.parent);
        GameObject railSpawn = Instantiate<GameObject>(railPref, spawnPos, spawnRota, spawnPoint.parent.parent);
        railsListController.AddRailIntoCurrentList(railSpawn);
        hasSpawn = true;
    }

    private void SpawnRailLeft(Transform spawnPoint)
    {
        Vector3 spawnPos = spawnPoint.position;
        Quaternion spawnRota = new Quaternion();
        spawnRota.eulerAngles = spawnPoint.localEulerAngles + spawnPoint.parent.eulerAngles;  // rotation = this rail rotation + NextLink0 rotation
        GameObject railPref;
        //do
        {
            railPref = railsList_Left[Random.Range(0, railsList_Left.Count)];
        } //while (railPref == PrefabUtility.GetCorrespondingObjectFromSource(gameObject) && railPref != null);   // check if the newly spawn rail is the same with this one
                                                                                                                  // Currently not working properly
        //GameObject railSpawn = Instantiate<GameObject>(railPref, spawnPos, spawnRota, transform.parent);
        GameObject railSpawn = Instantiate<GameObject>(railPref, spawnPos, spawnRota, spawnPoint.parent.parent);
        railsListController.AddRailIntoCurrentList(railSpawn);
        hasSpawn = true;
    }

    private void SpawnRailRight(Transform spawnPoint)
    {
        Vector3 spawnPos = spawnPoint.position;
        Quaternion spawnRota = new Quaternion();
        spawnRota.eulerAngles = spawnPoint.localEulerAngles + spawnPoint.parent.eulerAngles;  // rotation = this rail rotation + NextLink0 rotation
        GameObject railPref;
        //do
        {
            railPref = railsList_Right[Random.Range(0, railsList_Right.Count)];
        } //while (railPref == PrefabUtility.GetCorrespondingObjectFromSource(gameObject) && railPref != null);   // check if the newly spawn rail is the same with this one
                                                                                                                // Currently not working properly
        //GameObject railSpawn = Instantiate<GameObject>(railPref, spawnPos, spawnRota, transform.parent);
        GameObject railSpawn = Instantiate<GameObject>(railPref, spawnPos, spawnRota, spawnPoint.parent.parent);
        railsListController.AddRailIntoCurrentList(railSpawn);
        hasSpawn = true;
    }

    

}
