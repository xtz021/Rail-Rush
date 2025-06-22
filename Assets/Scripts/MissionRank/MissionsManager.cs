using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    public List<Mission> currentMissions;


    public void AddNewMission()
    {
        if (currentMissions.Count >= 3)
        {
            Debug.Log("Cannot add more than 3 missions at a time.");
            return;
        }
        Mission newMission;
        do
        {
            newMission = new Mission
            {
                type = (MissionType)Random.Range(0, System.Enum.GetValues(typeof(MissionType)).Length),
                targetValue = Random.Range(1, 100),
                currentValue = 0,
                isCompleted = false,
                pickAxesReward = Random.Range(1, 2)
            };
        } while (CheckContainsType(newMission.type));
        currentMissions.Add(newMission);
        Debug.Log($"Added new mission type {newMission.type.ToString()}");
    }

    private bool CheckContainsType(MissionType type)
    {
        foreach (Mission mission in currentMissions)
        {
            if (mission.type == type)
            {
                return true;
            }
        }
        return false;
    }
}
