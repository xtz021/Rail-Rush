using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionsUIController : MonoBehaviour
{
    public static MissionsUIController Instance { get; private set; }
    [Header("Mission UI Elements")]
    [SerializeField] private List<MissionUI> missionUIs;

    public bool isRankUp = false;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if (RankManager.Instance == null)
        {
            Debug.LogError("RankManager instance is null. Please ensure it is initialized before accessing MissonsUIController.");
            return;
        }
        if (MissionsManager.Instance == null)
        {
            Debug.LogError("MissionsManager instance is null. Please ensure it is initialized before accessing MissonsUIController.");
            return;
        }
        UpdateMissionUI();
    }

    private void UpdateMissionUI()
    {
        if(isRankUp)
        {
            GameMenuUIController.Instance.PopUpNotice($"You have been promoted to rank {RankManager.Instance.GetCurrentRank().rankName}!");
        }
        List<Mission> currentMissions = MissionsManager.Instance.currentMissions;
        for (int i = 0; i < missionUIs.Count; i++)
        {
            missionUIs[i].SetMissionIcon(currentMissions[i].icon);
            int currentProgress = currentMissions[i].currentValue;
            if (currentProgress > 0)
            {
                missionUIs[i].SetMissionDescription(currentMissions[i].missionDescription + $" {currentMissions[i].targetValue - currentMissions[i].currentValue} to go.");
            }
            else
            {
                missionUIs[i].SetMissionDescription(currentMissions[i].missionDescription);
            }
            missionUIs[i].SetMissionPickAxesReward(currentMissions[i].pickAxesReward);
            if (currentMissions[i].isCompleted)
            {
                missionUIs[i].SetMissionCompleted(true);
                UnityAction action = () =>                      // This action will be called when the mission button is clicked
                {
                    MissionsManager.Instance.CompleteMission(i);
                    UpdateMissionUI();
                };
                missionUIs[i].SetMissionButtonListener(action);
            }
        }
    }

}
