using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavesListController : MonoBehaviour
{
    [SerializeField] List<GameObject> cavesList_TurnLeft;
    [SerializeField] List<GameObject> cavesList_Straight_Big;
    [SerializeField] List<GameObject> cavesList_Straight_Med;
    [SerializeField] List<GameObject> cavesList_Straight_Small;
    [SerializeField] List<GameObject> cavesList_TurnRight;

    public List<GameObject> GetCaveListLeft()
    {
        return cavesList_TurnLeft;
    }

    public List<GameObject> GetCaveListStraight_Big()
    {
        return cavesList_Straight_Big;
    }

    public List<GameObject> GetCaveListStraight_Med()
    {
        return cavesList_Straight_Med;
    }

    public List<GameObject> GetCaveListStraight_Small()
    {
        return cavesList_Straight_Small;
    }

    public List<GameObject> GetCaveListRight()
    {
        return cavesList_TurnRight;
    }

}
