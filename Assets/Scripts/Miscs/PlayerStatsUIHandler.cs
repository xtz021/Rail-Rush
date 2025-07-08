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
        GameStatsController.LoadData();
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
        //tmp_AmethystCollected.text = "" + playerStats.AmethystCollected;
        tmp_AmethystCollected.text = "" + playerStats.GemCollectedByTypes[GemType.Amethyst];
        //tmp_GarnetCollected.text = "" + playerStats.GarnetCollected;
        tmp_GarnetCollected.text = "" + playerStats.GemCollectedByTypes[GemType.Garnet];
        //tmp_TopazCollected.text = "" + playerStats.TopazCollected;
        tmp_TopazCollected.text = "" + playerStats.GemCollectedByTypes[GemType.Topaz];
        //tmp_SpinelCollected.text = "" + playerStats.SpinelCollected;
        tmp_SpinelCollected.text = "" + playerStats.GemCollectedByTypes[GemType.Spinel];
        //tmp_RubyCollected.text = "" + playerStats.RubyCollected;
        tmp_RubyCollected.text = "" + playerStats.GemCollectedByTypes[GemType.Ruby];
        //tmp_SapphireCollected.text = "" + playerStats.SapphireCollected;
        tmp_SapphireCollected.text = "" + playerStats.GemCollectedByTypes[GemType.Sapphire];
        //tmp_EmeraldCollected.text = "" + playerStats.EmeraldCollected;
        tmp_EmeraldCollected.text = "" + playerStats.GemCollectedByTypes[GemType.Emerald];
        //tmp_DiamondCollected.text = "" + playerStats.DiamondCollected;
        tmp_DiamondCollected.text = "" + playerStats.GemCollectedByTypes[GemType.Diamond];
        tmp_TotalGemsCollected.text = "" + playerStats.TotalGemsCollected;
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
    //public int AmethystCollected;
    //public int GarnetCollected;
    //public int TopazCollected;
    //public int SpinelCollected;
    //public int RubyCollected;
    //public int SapphireCollected;
    //public int EmeraldCollected;
    //public int DiamondCollected;
    public int TotalGemsCollected;

    public Dictionary <GemType, int> GemCollectedByTypes = new Dictionary<GemType, int>()
    {
        { GemType.Amethyst, 0 },
        { GemType.Garnet, 0 },
        { GemType.Topaz, 0 },
        { GemType.Spinel, 0 },
        { GemType.Ruby, 0 },
        { GemType.Sapphire, 0 },
        { GemType.Emerald, 0 },
        { GemType.Diamond, 0 }
    };

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
        //AmethystCollected = 0;
        //GarnetCollected = 0;
        //TopazCollected = 0;
        //SpinelCollected = 0;
        //RubyCollected = 0;
        //SapphireCollected = 0;
        //EmeraldCollected = 0;
        //DiamondCollected = 0;
        GemCollectedByTypes[GemType.Amethyst] = 0;
        GemCollectedByTypes[GemType.Garnet] = 0;
        GemCollectedByTypes[GemType.Topaz] = 0;
        GemCollectedByTypes[GemType.Spinel] = 0;
        GemCollectedByTypes[GemType.Ruby] = 0;
        GemCollectedByTypes[GemType.Sapphire] = 0;
        GemCollectedByTypes[GemType.Emerald] = 0;
        GemCollectedByTypes[GemType.Diamond] = 0;
        TotalGemsCollected = 0;
    }
}