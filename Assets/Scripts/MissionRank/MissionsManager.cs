using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class MissionsManager : MonoBehaviour
{
    public static MissionsManager Instance { get; private set; }
    public List<Mission> currentMissions;

    private int maxMissions = 3; // Maximum number of missions allowed at a time

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        LoadCurrentMissionData();
    }



    private void OnDisable()
    {
        SaveCurrentMissionData();
    }

    public Mission GetMissionFromMissionData(MissionData missionData, Sprite sprite)
    {
        return new Mission
        {
            type = missionData.type,
            targetValue = missionData.targetValue,
            currentValue = 0,
            isCompleted = false,
            missionDescription = missionData.missionDescription,
            pickAxesReward = missionData.pickAxesReward,
            icon = sprite
        };
    }

    public bool IsExistInCurrentMissions(MissionType type)
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

    public void AddNewMission()
    {
        if (currentMissions.Count >= maxMissions)
        {
            Debug.Log("Cannot add more than 3 missions at a time.");
            return;
        }
        Mission newMission;
        do
        {
            int index = Random.Range(0, RankManager.Instance.GetCurrentRank().missions.Count);
            MissionData missionData = RankManager.Instance.GetCurrentRank().missions[index];
            Sprite icon = RankManager.Instance.GetMissionIcon(index);
            newMission = GetMissionFromMissionData(missionData,icon);
        } while (IsExistInCurrentMissions(newMission.type));           // Check if newMission type already exists
        currentMissions.Add(newMission);
        Debug.Log($"Added new mission type {newMission.type.ToString()}");
    }


    public void SaveCurrentMissionData()
    {
        MissionListWrapper wrapper = new MissionListWrapper { missions = currentMissions }; 
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("CurrentMissions", json);
        PlayerPrefs.Save();
    }


    public void LoadCurrentMissionData()
    {
        string json = PlayerPrefs.GetString("CurrentMissions", "");
        if (!string.IsNullOrEmpty(json))
        {
            MissionListWrapper wrapper = JsonUtility.FromJson<MissionListWrapper>(json);
            currentMissions = wrapper.missions;

            if (currentMissions == null || currentMissions.Count == 0)
            {
                currentMissions = new List<Mission>();
                FillCurrentMissions();
            }
            else
            {
                Debug.Log($"Loaded {currentMissions.Count} current missions from PlayerPrefs.");
                if (currentMissions.Count < 3)
                {
                    FillCurrentMissions();
                }
            }
        }
        else
        {
            ResetCurrentMissions();
        }
    }

    public void FillCurrentMissions()
    {
        while (currentMissions.Count < 3)
        {
            AddNewMission();
        }
        SaveCurrentMissionData();
    }

    public void ResetCurrentMissions()
    {
        currentMissions.Clear();
        PlayerPrefs.DeleteKey("CurrentMissions");
        FillCurrentMissions();
        Debug.Log("Current missions have been reset.");
    }
}

[System.Serializable]
public class MissionListWrapper             // For JSON saving since List<Mission> cannot be serialized directly
{
    public List<Mission> missions;
}