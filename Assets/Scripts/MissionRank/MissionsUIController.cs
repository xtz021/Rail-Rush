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
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject); // Destroy the previous instance if it exists
            Instance = this; // Set the new instance
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
        RemoveMissionButtonsEventListers();
        for (int i = 0; i < missionUIs.Count; i++)
        {
            //missionUIs[i].SetMissionIcon(currentMissions[i].icon);
            missionUIs[i].SetMissionIcon(RankManager.Instance.GetIconByMissionType(currentMissions[i].type));       // To avoid destroyed asset bug
            int currentProgress = currentMissions[i].currentValue;
            if (currentProgress > 0 && !currentMissions[i].isCompleted)
            {
                missionUIs[i].SetMissionDescription(currentMissions[i].missionDescription + $" {currentMissions[i].targetValue - currentProgress} to go.");
            }
            else
            {
                missionUIs[i].SetMissionDescription(currentMissions[i].missionDescription);
            }
            missionUIs[i].SetMissionPickAxesReward(currentMissions[i].pickAxesReward);
            if (currentMissions[i].isCompleted)
            {
                missionUIs[i].SetMissionIsCompletedUI(true);
                int capturedIndex = i; // Capture the current index for the closure
                UnityAction action = () =>                      // This action will be called when the mission button is clicked
                {
                    MissionsManager.Instance.CompleteMission(capturedIndex);
                    UpdateMissionUI();
                    RankingUIController.Instance.UpdateRankUI();
                };
                missionUIs[i].RemoveMissionButtonEventListerners();
                missionUIs[i].AddMissionButtonEventListener(action);
            }
            else
            {
                missionUIs[i].RemoveMissionButtonEventListerners();
                missionUIs[i].SetMissionIsCompletedUI(false);
            }
        }
    }

    private void RemoveMissionButtonsEventListers()
    {
        foreach (MissionUI missionUI in missionUIs)
        {
            missionUI.RemoveMissionButtonEventListerners();
        }
    }

}
