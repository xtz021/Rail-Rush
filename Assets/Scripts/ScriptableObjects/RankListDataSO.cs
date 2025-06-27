using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GemsData", menuName = "Scriptable Objects/Ranks Data", order = 4)]
public class RankListDataSO : ScriptableObject
{
    private static string KEY_CURRENT_RANKTIER = "_Rank_CurrentRankTier";
    private static string KEY_CURRENT_RANKPROGRESS = "_Rank_CurrentRankProgress";

    public List<Rank> Ranks;

    public List<Sprite> missionIcons;

    public int CurrentRankIndex;
    public int CurrentRankProgress;

    public void SaveCurrentRankData()
    {
        PlayerPrefs.SetInt(KEY_CURRENT_RANKTIER, CurrentRankIndex);
        PlayerPrefs.SetInt(KEY_CURRENT_RANKPROGRESS, CurrentRankProgress);
        PlayerPrefs.Save();
    }

    public void LoadCurrentRankData()
    {
        CurrentRankIndex = PlayerPrefs.GetInt(KEY_CURRENT_RANKTIER, 0);
        CurrentRankProgress = PlayerPrefs.GetInt(KEY_CURRENT_RANKPROGRESS, 0);
    }

    public void ResetRankData()
    {
        CurrentRankIndex = 0;
        CurrentRankProgress = 0;
        SaveCurrentRankData();
    }
}
