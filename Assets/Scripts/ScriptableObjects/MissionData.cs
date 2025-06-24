using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionData
{
    public MissionType type;
    public string missionDescription;
    public int targetValue;
    public int pickAxesReward = 1;
}

[System.Serializable]
public class Mission
{
    public MissionType type;
    public int targetValue;
    public int currentValue;
    public string missionDescription;
    public bool isCompleted;
    public int pickAxesReward;
    public Sprite icon;
}

public enum MissionType
{
    Play,
    CollectGems,
    CollectNuggets,
    TravelDistance,
    Jump
}
