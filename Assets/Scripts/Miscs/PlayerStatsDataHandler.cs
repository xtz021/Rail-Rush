using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PlayerStatsDataHandler : MonoBehaviour
{
    public static PlayerStats playerStats;

    [Header("Stats UI Text")]
    [SerializeField] TMP_Text tmp_BestRun;
    [SerializeField] TMP_Text tmp_NuggetsCollected;
    [SerializeField] TMP_Text tmp_MaxNuggetsCollectedInAGame;
    [SerializeField] TMP_Text tmp_TotalPlays;
    [SerializeField] TMP_Text tmp_TotalMissionsCompleted;
    [Space(20f)]
    [SerializeField] TMP_Text tmp_Deaths;
    [SerializeField] TMP_Text tmp_DeathsByUpperObs;
    [SerializeField] TMP_Text tmp_DeathsByLowerObs;
    [SerializeField] TMP_Text tmp_DeathsByRightObs;
    [SerializeField] TMP_Text tmp_DeathsByleftObs;
    [Space(20f)]
    [SerializeField] TMP_Text tmp_AmethystCollected;
    [SerializeField] TMP_Text tmp_GarnetCollected;
    [SerializeField] TMP_Text tmp_TopazCollected;
    [SerializeField] TMP_Text tmp_SpinelCollected;
    [SerializeField] TMP_Text tmp_RubyCollected;
    [SerializeField] TMP_Text tmp_SapphireCollected;
    [SerializeField] TMP_Text tmp_EmeraldCollected;
    [SerializeField] TMP_Text tmp_DiamondCollected;
    [SerializeField] TMP_Text tmp_TotalGemsCollected;

    private void Awake()
    {
        LoadData();
    }

    private void OnEnable()
    {
        UpdateUITexts();
    }


    public void UpdateUITexts()
    {
        if (playerStats == null)
        {
            Debug.LogWarning("PlayerStats is null, initializing new PlayerStats.");
            playerStats = new PlayerStats();
        }
        tmp_BestRun.text = "" + playerStats.BestRun + "m";
        tmp_NuggetsCollected.text = "" + playerStats.NuggetsCollected;
        tmp_MaxNuggetsCollectedInAGame.text = "" + playerStats.MaxNuggetsCollectedInAGame;
        tmp_TotalPlays.text = "" + playerStats.TotalPlays;
        tmp_TotalMissionsCompleted.text = "" + playerStats.TotalMissionsCompleted;
        tmp_Deaths.text = "" + playerStats.Deaths;
        tmp_DeathsByUpperObs.text = "" + playerStats.DeathsByUpperObs;
        tmp_DeathsByLowerObs.text = "" + playerStats.DeathsByLowerObs;
        tmp_DeathsByRightObs.text = "" + playerStats.DeathsByRightObs;
        tmp_DeathsByleftObs.text = "" + playerStats.DeathsByLeftObs;
        tmp_AmethystCollected.text = "" + playerStats.AmethystCollected;
        tmp_GarnetCollected.text = "" + playerStats.GarnetCollected;
        tmp_TopazCollected.text = "" + playerStats.TopazCollected;
        tmp_SpinelCollected.text = "" + playerStats.SpinelCollected;
        tmp_RubyCollected.text = "" + playerStats.RubyCollected;
        tmp_SapphireCollected.text = "" + playerStats.SapphireCollected;
        tmp_EmeraldCollected.text = "" + playerStats.EmeraldCollected;
        tmp_DiamondCollected.text = "" + playerStats.DiamondCollected;
        tmp_TotalGemsCollected.text = "" + playerStats.TotalGemsCollected;
    }    

    public static void SaveData()
    {
        string json = JsonUtility.ToJson(playerStats);
        string path = Path.Combine(Application.persistentDataPath, "playerStats.json");
        File.WriteAllText(path, json);
        Debug.Log("Player stats saved to: " + path);
    }

    public static void LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerStats.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, playerStats);
            Debug.Log("Player stats loaded from: " + path);
        }
        else
        {
            playerStats = new PlayerStats();
            Debug.Log(path + " not found, initializing new player stats.");
        }
    }
}

public class PlayerStats
{
    public int BestRun;
    public int NuggetsCollected;
    public int MaxNuggetsCollectedInAGame;
    public int TotalPlays;
    public int TotalMissionsCompleted;
    public int Deaths;
    public int DeathsByUpperObs;
    public int DeathsByLowerObs;
    public int DeathsByRightObs;
    public int DeathsByLeftObs;
    public int AmethystCollected;
    public int GarnetCollected;
    public int TopazCollected;
    public int SpinelCollected;
    public int RubyCollected;
    public int SapphireCollected;
    public int EmeraldCollected;
    public int DiamondCollected;
    public int TotalGemsCollected;

    public PlayerStats()
    {
        BestRun = 0;
        NuggetsCollected = 0;
        MaxNuggetsCollectedInAGame = 0;
        TotalPlays = 0;
        TotalMissionsCompleted = 0;
        Deaths = 0;
        DeathsByUpperObs = 0;
        DeathsByLowerObs = 0;
        DeathsByRightObs = 0;
        DeathsByLeftObs = 0;
        AmethystCollected = 0;
        GarnetCollected = 0;
        TopazCollected = 0;
        SpinelCollected = 0;
        RubyCollected = 0;
        SapphireCollected = 0;
        EmeraldCollected = 0;
        DiamondCollected = 0;
        TotalGemsCollected = 0;
    }
}