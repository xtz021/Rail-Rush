using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    public static RankManager Instance {  get; private set; }

    public RankListDataSO ranksData;

    public Sprite pickAxeProgressIcon_Enable;
    public Sprite pickAxeProgressIcon_Disable;

    private void Awake()
    {
        //if (Instance != null && Instance != this)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject); // Destroy the previous instance if it exists
            Debug.LogWarning("Multiple instances of RankManager detected. Destroying older duplicate instance.");
            Instance = this; // Set the new instance
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        ranksData.LoadCurrentRankData();
    }

    private void Start()
    {
        
    }

    private void OnDisable()
    {
        SaveCurrentRankData();
    }

    public void SaveCurrentRankData()
    {
        ranksData.SaveCurrentRankData();
    }

    public void LoadCurrentRankData()
    {
        ranksData.LoadCurrentRankData();
    }

    public void ResetRankData()
    {
        ranksData.ResetRankData();
        SaveCurrentRankData();
    }

    public Rank GetCurrentRank()
    {
        if (ranksData.CurrentRankIndex < 0 || ranksData.CurrentRankIndex >= ranksData.Ranks.Count)
        {
            Debug.LogError("Current rank index is out of bounds.");
            return null;
        }
        return ranksData.Ranks[ranksData.CurrentRankIndex];
    }

    public void GainCurrentRankProgress(int pickAxes)
    {
        ranksData.CurrentRankProgress += pickAxes;
        if (ranksData.CurrentRankProgress >= GetCurrentRank().pickAxesRequired)     // Check for rank promotion
        {
            ranksData.CurrentRankProgress = 0;
            ranksData.CurrentRankIndex++;
            if (ranksData.CurrentRankIndex >= ranksData.Ranks.Count)
            {
                ranksData.CurrentRankIndex = ranksData.Ranks.Count - 1; // Cap at max rank
            }
            else
            {
                int goldEarned = GetCurrentRank().NuggetReward;
                InventoryManager.Instance.inventory.GainGold(goldEarned);
                GameMenuUIController.Instance.PopUpNotice($"Promoted to {GetCurrentRank().rankName} and earned {goldEarned}!");
                RankingUIController.Instance.UpdateRankUI();
            }
        }
        SaveCurrentRankData();
    }

    public int GetCurrentRankIndex()
    {
        return ranksData.CurrentRankIndex;
    }

    public int GetNextRankIndex()
    {
        int nextRankIndex = ranksData.CurrentRankIndex + 1;
        if (nextRankIndex >= ranksData.Ranks.Count)
        {
            nextRankIndex = ranksData.Ranks.Count - 1; // Cap at max rank
        }
        return nextRankIndex;
    }

    public int GetCurrentRankProgress()
    {
        return ranksData.CurrentRankProgress;
    }

    public Sprite GetMissionIcon(int index)
    {
        if (ranksData.CurrentRankIndex < 0 || ranksData.CurrentRankIndex >= ranksData.missionIcons.Count)
        {
            Debug.LogError("Current rank index is out of bounds for icons.");
            return null;
        }
        return ranksData.missionIcons[index];
    }


    public Sprite GetIconByMissionType(MissionType type)
    {
        Rank currentRank = GetCurrentRank();
        if (currentRank == null)
        {
            Debug.LogError("Current rank is null. Cannot get icon by mission type.");
            return null;
        }
        if (ranksData.missionIcons.Count == 0)
        {
            Debug.LogWarning("No mission icons available.");
            return null; // Return null if no icons are available
        }
        MissionData mission = currentRank.missions.Find(m => m.type == type);
        if(mission != null)
        {
            int index = (int)mission.type;
            if (index >= 0 && index < ranksData.missionIcons.Count)
            {
                return ranksData.missionIcons[index];
            }
        }
        Debug.LogWarning($"No mission of type {type} found to get icon.");
        return null; // Return null if no matching mission is found
    }

    public void RankUp()
    {
        if(ranksData.CurrentRankIndex <  ranksData.Ranks.Count - 1)
        {
            ranksData.CurrentRankIndex++;
            if(RankingUIController.Instance != null)
            {
                if(RankingUIController.Instance.gameObject.activeSelf)
                {
                    RankingUIController.Instance.UpdateRankUI();
                }
            }
        }

    }

}
