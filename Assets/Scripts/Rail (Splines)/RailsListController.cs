using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailsListController : MonoBehaviour
{
    public Queue<GameObject> currentRailList;

    [SerializeField] List<GameObject> railsList_Left;
    [SerializeField] List<GameObject> railsList_Center;
    [SerializeField] List<GameObject> railsList_Right;

    int railLimit = 4;

    private void Update()
    {
        CurrentRailListLimitCheck();
    }

    public void CurrentRailListLimitCheck()
    {
        while (currentRailList.Count > railLimit)
        {
            GameObject outtedGameobject = new GameObject();
            currentRailList.TryDequeue(out outtedGameobject);
            Destroy(outtedGameobject);
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

}
