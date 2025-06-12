using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GameStatsController : MonoBehaviour
{
    public static GameStatsController Instance { get; private set; }

    public const string KEY_BESTDISTANT = "_Best_Distant";
    public const string KEY_BESTGOLD = "_Best_Gold";


    private int _best_Distance = 0;
    private int _best_Gold = 0;

    public bool openShopFromGame { get; private set; } = false;


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

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        GetSaveGameValues();
    }

    private void GetSaveGameValues()
    {
        _best_Distance = PlayerPrefs.GetInt(KEY_BESTDISTANT, 0);
        _best_Gold = PlayerPrefs.GetInt(KEY_BESTGOLD, 0);
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt(GameStatsController.KEY_BESTDISTANT, _best_Distance);
        PlayerPrefs.SetInt(GameStatsController.KEY_BESTGOLD, _best_Gold);
        PlayerPrefs.Save();
        GetSaveGameValues();
    }

    public void SetNewBestGold(int goldCount)
    {
        if (_best_Gold <= goldCount)
        {
            _best_Gold = goldCount;
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
        }
        else
        {
            Debug.LogWarning("New distance count is not greater than the current best distance count.");
        }
    }


    public void OpenShopFromGame()
    {
        openShopFromGame = true;
    }

    public void ResetOpenShop()
    {
        openShopFromGame = false;
    }
}
