using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    private int totalGold = 0;
    private int best_Distant = 0;
    public static int GoldCount;
    public static int DistantCount;

    private bool isProgressSaved = false;
    private const string KEY_TOTALGOLD = "_TotalGold";
    private const string KEY_BESTDISTANT = "_Best_Distant";


    private void Start()
    {
        GetStartGameValues();
    }

    private void GetStartGameValues()
    {
        GoldCount = 0;
        DistantCount = 0;
        isProgressSaved = false;
        totalGold = PlayerPrefs.GetInt(KEY_TOTALGOLD, 0);
        best_Distant = PlayerPrefs.GetInt(KEY_BESTDISTANT, 0);
    }

    public void SaveProgress()
    {
        if (!isProgressSaved)
        {
            totalGold += GoldCount;
            PlayerPrefs.SetInt(KEY_TOTALGOLD, totalGold);
            if(DistantCount > best_Distant)
            {
                PlayerPrefs.SetInt(KEY_BESTDISTANT,DistantCount);
            }
        }
    }


}
