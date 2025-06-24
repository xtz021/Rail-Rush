using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    [SerializeField] private Image missionIcon;
    [SerializeField] private TMP_Text missionDescription;
    [SerializeField] private List<GameObject> missionRewardIcons;

    public void SetMissionIcon(Sprite icon)
    {
        if (missionIcon != null)
        {
            missionIcon.sprite = icon;
        }
        else
        {
            Debug.LogError("Mission Icon Image is not assigned.");
        }
    }

    public void SetMissionDescription(string description)
    {
        if (missionDescription != null)
        {
            missionDescription.text = description;
        }
        else
        {
            Debug.LogError("Mission Description Text is not assigned.");
        }
    }

    public void SetMissionPickAxesReward(int pickAxesReward)
    {
        if (pickAxesReward <= 0)
        {
            pickAxesReward = 1;
            Debug.LogWarning("Pick Axes Reward is set to a value less than or equal to zero. Defaulting to 1.");
        }
        if (missionRewardIcons != null && missionRewardIcons.Count > 0)
        {
            for (int i = 0; i < missionRewardIcons.Count; i++)
            {
                if (i < pickAxesReward)
                {
                    missionRewardIcons[i].SetActive(true);
                }
                else
                {
                    missionRewardIcons[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("Mission Reward Icons are not assigned or empty.");
        }
    }
}
