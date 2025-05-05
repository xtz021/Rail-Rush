using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavesListController : MonoBehaviour
{
    [SerializeField] List<GameObject> cavesList_TurnLeft;
    [SerializeField] List<GameObject> cavesList_Straight;
    [SerializeField] List<GameObject> cavesList_TurnRight;

    public List<GameObject> GetCaveListLeft()
    {
        return cavesList_TurnLeft;
    }

    public List<GameObject> GetCaveListStraight()
    {
        return cavesList_Straight;
    }

    public List<GameObject> GetCaveListRight()
    {
        return cavesList_TurnRight;
    }

}
