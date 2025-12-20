using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuggetSpawner : MonoBehaviour
{
    private const string STRING_TAG_NUGGETSPAWNPOINT = "NuggetSpawnPoint";
    private List<GameObject> nuggetSpawnPointsList;

    void Start()
    {
        SpawnNuggets();
    }


    private void SpawnNuggets()
    {
        nuggetSpawnPointsList = CheckAndGetNuggetsList();
        if(nuggetSpawnPointsList.Count > 0)
        {
            foreach (GameObject spawnPoint in nuggetSpawnPointsList)
            {
                GameObject nugget = PoolManager.Instance.nuggetPool.Get();
                nugget.transform.position = spawnPoint.transform.position;
                nugget.transform.rotation = spawnPoint.transform.rotation;
                nugget.transform.SetParent(transform.parent);
            }
        }
    }


    private List<GameObject> CheckAndGetNuggetsList()
    {
        List<GameObject> NuggetList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.tag == STRING_TAG_NUGGETSPAWNPOINT)
            {
                NuggetList.Add(child.gameObject);
            }
        }
        return NuggetList;
    }
}
