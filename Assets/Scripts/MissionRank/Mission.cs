using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Mission
{
    public MissionType type;
    public int targetValue;
    public int currentValue;
    public string missionDescription;
    public bool isCompleted;
    public int pickAxesReward;
}

public enum MissionType
{
    Play,
    ClooectGems,
    CollectCoins,
    TravelDistance,
    DuckObstacles,
    Jump,
    UseCartStuffItem
}
