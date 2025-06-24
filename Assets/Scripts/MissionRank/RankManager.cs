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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        ranksData.LoadCurrentRankData();
    }

    private void Start()
    {
        
    }

    public void SaveCurrentRankData()
    {
        ranksData.SaveCurrentRankData();
    }

    public void LoadCurrentRankData()
    {
        ranksData.LoadCurrentRankData();
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

}
