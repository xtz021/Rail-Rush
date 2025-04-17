using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailSpawner : MonoBehaviour
{
    List<GameObject> railsList_Left;
    List<GameObject> railsList_Center;
    List<GameObject> railsList_Right;

    bool hasSpawn;

    private void Start()
    {
        hasSpawn = false;
        railsList_Center = transform.parent.GetComponent<RailsListController>().railsList_Center;
        railsList_Left = transform.parent.GetComponent<RailsListController>().railsList_Left;
        railsList_Right = transform.parent.GetComponent<RailsListController>().railsList_Right;
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
        Transform nextSpawnPoint = transform.Find("NextLink0");
        List<Transform> endPoints = new List<Transform>();          // Points where the rail ends, can end with multiple rail on left, right and center
        foreach (Transform child in transform)
        {
            if (child.tag == "EndPoint")
            {
                endPoints.Add(child);
            }
        }
        if (endPoints.Count >= 1)
        {
            int endPointIndex = Random.Range(0, endPoints.Count);   // Get end point to spawn using an suitable rail list
            if (endPoints[endPointIndex].localPosition.x == 0)
            {
                SpawnRailCenter(nextSpawnPoint);
            }
            else if (endPoints[endPointIndex].localPosition.x > 0)
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
        spawnRota.eulerAngles = spawnPoint.localEulerAngles + transform.eulerAngles;  // rotation = this rail rotation + NextLink0 rotation
        GameObject railPref = railsList_Center[Random.Range(0, railsList_Center.Count)];
        GameObject railSpawn = Instantiate<GameObject>(railPref, spawnPos, spawnRota, transform.parent);
        hasSpawn = true;
    }

    private void SpawnRailLeft(Transform spawnPoint)
    {
        Vector3 spawnPos = spawnPoint.position;
        Quaternion spawnRota = new Quaternion();
        spawnRota.eulerAngles = spawnPoint.localEulerAngles + transform.eulerAngles;  // rotation = this rail rotation + NextLink0 rotation
        GameObject railPref = railsList_Left[Random.Range(0, railsList_Left.Count)];
        GameObject railSpawn = Instantiate<GameObject>(railPref, spawnPos, spawnRota, transform.parent);
        hasSpawn = true;
    }

    private void SpawnRailRight(Transform spawnPoint)
    {
        Vector3 spawnPos = spawnPoint.position;
        Quaternion spawnRota = new Quaternion();
        spawnRota.eulerAngles = spawnPoint.localEulerAngles + transform.eulerAngles;  // rotation = this rail rotation + NextLink0 rotation
        GameObject railPref = railsList_Right[Random.Range(0, railsList_Right.Count)];
        GameObject railSpawn = Instantiate<GameObject>(railPref, spawnPos, spawnRota, transform.parent);
        hasSpawn = true;
    }

    

}
