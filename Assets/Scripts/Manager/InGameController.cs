using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    public static InGameController Instance { get; private set; }
    public static int GoldCount;
    public static int DistanceCount;

    private int totalGold = 0;
    private int best_Distance = 0;
    private bool isProgressSaved = false;
    private const string KEY_TOTALGOLD = "_TotalGold";
    private const string KEY_BESTDISTANT = "_Best_Distant";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple instances of InGameController detected. Destroying duplicate.");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GetStartGameValues();
    }

    private void GetStartGameValues()
    {
        GoldCount = 0;
        DistanceCount = 0;
        isProgressSaved = false;
        totalGold = PlayerPrefs.GetInt(KEY_TOTALGOLD, 0);
        best_Distance = PlayerPrefs.GetInt(KEY_BESTDISTANT, 0);
    }

    public void SaveProgress()
    {
        if (!isProgressSaved)
        {
            totalGold += GoldCount;
            PlayerPrefs.SetInt(KEY_TOTALGOLD, totalGold);
            if(DistanceCount > best_Distance)
            {
                PlayerPrefs.SetInt(KEY_BESTDISTANT,DistanceCount);
            }
        }
    }


}
