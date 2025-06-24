using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingUIController : MonoBehaviour
{
    public TMP_Text currentRank_text;
    public TMP_Text nextRank_text;
    public List<Image> currentRank_Progress_Images;

    private const string CurrentRankBaseText = "Current rank - ";
    private const string NextRankBaseText = "Next rank - ";

    private void Start()
    {
        if (RankManager.Instance == null)
        {
            Debug.LogError("RankManager instance is null. Please ensure it is initialized before accessing RankingUIController.");
            return;
        }
    }

    private void OnEnable()
    {
        UpdateRankUI();
    }

    public void UpdateRankUI()
    {
        SetRankTexts();
        SetProgressUI();
    }

    private void SetRankTexts()
    {
        int currentRankIndex = RankManager.Instance.GetCurrentRankIndex();
        int nextRankIndex = RankManager.Instance.GetNextRankIndex();
        if (nextRankIndex <= currentRankIndex)
        {
            nextRank_text.text = NextRankBaseText + "Max Rank Reached";
        }
        else
        {
            nextRank_text.text = NextRankBaseText + RankManager.Instance.ranksData.Ranks[nextRankIndex].rankName;
        }
        currentRank_text.text = CurrentRankBaseText + RankManager.Instance.GetCurrentRank().rankName;
    }

    private void SetProgressUI()
    {
        int currentRankProgress = RankManager.Instance.GetCurrentRankProgress();
        int pickAxesRequired = RankManager.Instance.GetCurrentRank().pickAxesRequired;
        for(int i = 0; i < currentRank_Progress_Images.Count; i++)
        {
            if (i < pickAxesRequired)
            {
                currentRank_Progress_Images[i].gameObject.SetActive(true);
            }
            else
            {
                currentRank_Progress_Images[i].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < pickAxesRequired; i++)
        {
            if (i < currentRankProgress)
            {
                currentRank_Progress_Images[i].sprite = RankManager.Instance.pickAxeProgressIcon_Enable;
            }
            else
            {
                currentRank_Progress_Images[i].sprite = RankManager.Instance.pickAxeProgressIcon_Disable;
            }
        }

    }

}
