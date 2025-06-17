using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PlayerStatsUIHandler : MonoBehaviour
{

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
        if (GameStatsController.Instance.playerStats == null)
        {
            Debug.LogWarning("PlayerStats is null, initializing new PlayerStats.");
            GameStatsController.Instance.playerStats = new PlayerStats();
        }
        PlayerStats playerStats = GameStatsController.Instance.playerStats;
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

    public static void SaveData(PlayerStats playerStats)
    {
        PlayerPrefs.SetInt("Stats_BestRun", playerStats.BestRun);
        PlayerPrefs.SetInt("Stats_NuggetsCollected", playerStats.NuggetsCollected);
        PlayerPrefs.SetInt("Stats_MaxNuggetsCollectedInAGame", playerStats.MaxNuggetsCollectedInAGame);
        PlayerPrefs.SetInt("Stats_TotalPlays", playerStats.TotalPlays);
        PlayerPrefs.SetInt("Stats_TotalMissionsCompleted", playerStats.TotalMissionsCompleted);
        PlayerPrefs.SetInt("Stats_Deaths", playerStats.Deaths);
        PlayerPrefs.SetInt("Stats_DeathsByUpperObs", playerStats.DeathsByUpperObs);
        PlayerPrefs.SetInt("Stats_DeathsByLowerObs", playerStats.DeathsByLowerObs);
        PlayerPrefs.SetInt("Stats_DeathsByRightObs", playerStats.DeathsByRightObs);
        PlayerPrefs.SetInt("Stats_DeathsByLeftObs", playerStats.DeathsByLeftObs);
        PlayerPrefs.SetInt("Stats_AmethystCollected", playerStats.AmethystCollected);
        PlayerPrefs.SetInt("Stats_GarnetCollected", playerStats.GarnetCollected);
        PlayerPrefs.SetInt("Stats_TopazCollected", playerStats.TopazCollected);
        PlayerPrefs.SetInt("Stats_SpinelCollected", playerStats.SpinelCollected);
        PlayerPrefs.SetInt("Stats_RubyCollected", playerStats.RubyCollected);
        PlayerPrefs.SetInt("Stats_SapphireCollected", playerStats.SapphireCollected);
        PlayerPrefs.SetInt("Stats_EmeraldCollected", playerStats.EmeraldCollected);
        PlayerPrefs.SetInt("Stats_DiamondCollected", playerStats.DiamondCollected);
        PlayerPrefs.SetInt("Stats_TotalGemsCollected", playerStats.TotalGemsCollected);
        PlayerPrefs.Save();

    }

    public static PlayerStats LoadData()
    {
        PlayerStats playerStats = new PlayerStats();
        playerStats.BestRun = PlayerPrefs.GetInt("Stats_BestRun", 0);
        playerStats.NuggetsCollected = PlayerPrefs.GetInt("Stats_NuggetsCollected", 0);
        playerStats.MaxNuggetsCollectedInAGame = PlayerPrefs.GetInt("Stats_MaxNuggetsCollectedInAGame", 0);
        playerStats.TotalPlays = PlayerPrefs.GetInt("Stats_TotalPlays", 0);
        playerStats.TotalMissionsCompleted = PlayerPrefs.GetInt("Stats_TotalMissionsCompleted", 0);
        playerStats.Deaths = PlayerPrefs.GetInt("Stats_Deaths", 0);
        playerStats.DeathsByUpperObs = PlayerPrefs.GetInt("Stats_DeathsByUpperObs", 0);
        playerStats.DeathsByLowerObs = PlayerPrefs.GetInt("Stats_DeathsByLowerObs", 0);
        playerStats.DeathsByRightObs = PlayerPrefs.GetInt("Stats_DeathsByRightObs", 0);
        playerStats.DeathsByLeftObs = PlayerPrefs.GetInt("Stats_DeathsByLeftObs", 0);
        playerStats.AmethystCollected = PlayerPrefs.GetInt("Stats_AmethystCollected", 0);
        playerStats.GarnetCollected = PlayerPrefs.GetInt("Stats_GarnetCollected", 0);
        playerStats.TopazCollected = PlayerPrefs.GetInt("Stats_TopazCollected", 0);
        playerStats.SpinelCollected = PlayerPrefs.GetInt("Stats_SpinelCollected", 0);
        playerStats.RubyCollected = PlayerPrefs.GetInt("Stats_RubyCollected", 0);
        playerStats.SapphireCollected = PlayerPrefs.GetInt("Stats_SapphireCollected", 0);
        playerStats.EmeraldCollected = PlayerPrefs.GetInt("Stats_EmeraldCollected", 0);
        playerStats.DiamondCollected = PlayerPrefs.GetInt("Stats_DiamondCollected", 0);
        playerStats.TotalGemsCollected = PlayerPrefs.GetInt("Stats_TotalGemsCollected", 0);
        return playerStats;
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