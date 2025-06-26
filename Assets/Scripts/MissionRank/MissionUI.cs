using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    [SerializeField] private Button missionButton;
    [SerializeField] private Image missionIcon;
    [SerializeField] private TMP_Text missionDescription;
    [SerializeField] private List<GameObject> missionRewardIcons;
    [SerializeField] private Image completedIcon;

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

    public void RemoveMissionButtonEventListerners()
        {
        if (missionButton != null)
        {
            missionButton.onClick.RemoveAllListeners();
        }
        else
        {
            Debug.LogWarning("Mission Button is not assigned.");
        }
    }


    public void AddMissionButtonEventListener(UnityAction action)
    {
        if (missionButton != null)
        {
            missionButton.onClick.AddListener(action);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.LogError("Mission Button is not assigned.");
        }
    }

    public void SetMissionIsCompletedUI(bool isCompleted)
    {
        if (completedIcon != null)
        {
            completedIcon.gameObject.SetActive(isCompleted);
            Debug.Log($"Mission completion status set to: {isCompleted}");
        }
        else
        {
            Debug.LogError("Completed Icon Image is not assigned.");
        }
    }
}
