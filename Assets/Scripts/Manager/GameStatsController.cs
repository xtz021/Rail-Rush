using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GameStatsController : MonoBehaviour
{
    public static GameStatsController Instance { get; private set; }

    public PlayerStats playerStats;

    public const string KEY_BESTDISTANT = "_Best_Distant";
    public const string KEY_BESTGOLD = "_Best_Gold";


    private int _best_Distance = 0;
    private int _best_Gold = 0;

    public static bool openShopFromGame = false;
    public static bool openMissionsFromGame = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("Multiple instances of GameStatsController detected. Destroying duplicate instance.");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        playerStats = GameStatsController.LoadData();
    }

    private void OnDisable()
    {
        if(Instance != null && Instance != this)
        {
            return;
        }
        if (playerStats != null)
        {
            SaveData(playerStats);
        }
        else
        {
            Debug.LogWarning("PlayerStats is null, cannot save data.");
        }
    }

    public int best_Distance
    {
        get
        {
            return this._best_Distance;
        }
        private set
        {
            this._best_Distance = value;
        }
    }

    public int best_Gold
    {
        get
        {
            return this._best_Gold;
        }
        private set
        {
            this._best_Gold = value;
        }
    }


    private void Start()
    {
        GetSaveGameValues();
    }

    private void GetSaveGameValues()
    {
        _best_Distance = playerStats.BestRun;
        _best_Gold = playerStats.MaxNuggetsCollectedInAGame;
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
        Debug.Log("GameStatsController: Loaded player stats from PlayerPrefs.");
        return playerStats;
    }

    public void SetNewBestGold(int goldCount)
    {
        if (_best_Gold <= goldCount)
        {
            _best_Gold = goldCount;
            playerStats.MaxNuggetsCollectedInAGame = _best_Gold;
            if (InGameController.Instance != null)
            {
                InGameController.Instance.UpdateBestScores();
            }
            else
            {
                Debug.LogWarning("InGameController instance is null, cannot update best gold in-game.");
            }
        }
        else
        {
            Debug.LogWarning("New gold count is not greater than the current best gold count.");
        }
    }
    public void SetNewBestDistance(int distanceCount)
    {
        if (_best_Distance <= distanceCount)
        {
            _best_Distance = distanceCount;
            playerStats.BestRun = _best_Distance;
            if(InGameController.Instance != null)
            {
                InGameController.Instance.UpdateBestScores();
            }
            else
            {
                Debug.LogWarning("InGameController instance is null, cannot update best distance in-game.");
            }
        }
        else
        {
            Debug.LogWarning("New distance count is not greater than the current best distance count.");
        }
    }


    public void OpenShopFromGame()
    {
        openShopFromGame = true;
        openMissionsFromGame = false;
    }


    public void ResetOpenShop()
    {
        openShopFromGame = false;
    }

    public void OpenMissionsFromGame()
    {
        openMissionsFromGame = true;
        openShopFromGame = false;
    }

    public void ResetOpenMission()
    {
        openMissionsFromGame = false;
    }
}
