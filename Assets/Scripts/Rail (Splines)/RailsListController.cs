using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailsListController : MonoBehaviour
{
    public Queue<GameObject> currentRailList;
    [SerializeField] GameObject lastRailInQueue;

    [SerializeField] List<GameObject> railsList_Left;
    [SerializeField] List<GameObject> railsList_Center;
    [SerializeField] List<GameObject> railsList_Right;

    int railLimit = 5;

    private void Start()
    {
        currentRailList = new Queue<GameObject>();
        AddAllCurrentRailsIntoList();
    }


    private void AddAllCurrentRailsIntoList()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.name.StartsWith("B"))
            {
                AddRailIntoCurrentList(child.gameObject);
            }
        }
    }

    public void AddRailIntoCurrentList(GameObject rail)
    {
        currentRailList.Enqueue(rail);
        lastRailInQueue = rail;
        //Debug.Log($"Added rail: {rail.name}. Current list count: {currentRailList.Count}");
        CurrentRailListLimitCheck();
    }

    public void CurrentRailListLimitCheck()
    {
        while (currentRailList.Count > railLimit)
        {
            GameObject outtedGameobject;
            currentRailList.TryDequeue(out outtedGameobject);
            if(outtedGameobject.TryGetComponent<RailSpawner>(out RailSpawner railSpawner))
            {
                foreach (Transform child in outtedGameobject.transform)
                {
                    if (child.tag == "Bonus" && child.name.Contains("Nugget"))
                    {
                        child.transform.SetParent(null);
                        child.gameObject.SetActive(false);
                    }
                }
            }
            Destroy(outtedGameobject);
            //Debug.Log("Removed rail: " + outtedGameobject.name);
        }
    }


    public List<GameObject> GetRailListLeft()
    {
        return railsList_Left;
    }

    public List<GameObject> GetRailListStraight()
    {
        return railsList_Center;
    }

    public List<GameObject> GetRailListRight()
    {
        return railsList_Right;
    }

    public Queue<GameObject> GetCurrentRailList()
    {
        return currentRailList;
    }

    public GameObject GetLastRailInQueue()
    {
        return lastRailInQueue;
    }

}
