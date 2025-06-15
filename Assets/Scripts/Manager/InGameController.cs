using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    public static InGameController Instance { get; private set; }

    public int Current_GoldCount;
    public int Current_DistanceCount;

    [Header("Variables")]
    public int saveMeBoxPopupCount = 0;

    [Header("Do not need input")]
    public GameObject lastRailEntered;

    [Header("Need input")]
    [SerializeField] private GameObject saveMeUIBoxFreeAd;
    [SerializeField] private GameObject gameOverPanel;

    //private int _totalGold = 0;
    private int _best_Distance = 0;
    private int _best_Gold = 0;
    private bool isProgressSaved = false;
    private Transform player;

    //public int totalGold
    //{
    //    get
    //    {
    //        return this._totalGold;
    //    }
    //    private set
    //    {
    //        this._totalGold = value;
    //    }
    //}

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
        player = PlayerCartMovement.Instance.transform;
        saveMeBoxPopupCount = 0;
    }

    private void Update()
    {
        SaveMePanelCheck();
    }

    private void GetStartGameValues()
    {
        Current_GoldCount = 0;
        Current_DistanceCount = 0;
        isProgressSaved = false;
        //_totalGold = PlayerPrefs.GetInt(GameStatsController.KEY_TOTALGOLD, 0);
        _best_Distance = PlayerPrefs.GetInt(GameStatsController.KEY_BESTDISTANT, 0);
        _best_Gold = PlayerPrefs.GetInt(GameStatsController.KEY_BESTGOLD, 0);
    }

    public void SaveProgress()
    {
        if (!isProgressSaved)
        {
            //_totalGold += Current_GoldCount;
            //PlayerPrefs.SetInt(GameStatsController.KEY_TOTALGOLD, _totalGold);
            if (Current_DistanceCount > _best_Distance)
            {
                GameStatsController.Instance.SetNewBestDistance(Current_DistanceCount);
                GooglePlayGamesController.Instance.PostLeaderBoardDistanceScore(Current_DistanceCount);
            }
            if (Current_GoldCount > _best_Gold)
            {
                GameStatsController.Instance.SetNewBestGold(Current_GoldCount);
                GooglePlayGamesController.Instance.PostLeaderBoardGoldScore(Current_GoldCount);
            }
            GameStatsController.Instance.SaveProgress();
            isProgressSaved = true;
            //_totalGold = PlayerPrefs.GetInt(GameStatsController.KEY_TOTALGOLD, 0);
            _best_Distance = PlayerPrefs.GetInt(GameStatsController.KEY_BESTDISTANT, 0);
            _best_Gold = PlayerPrefs.GetInt(GameStatsController.KEY_BESTGOLD, 0);
        }
    }

    public void GainGold(int gold)
    {
        Current_GoldCount += gold;
        InGameUIController.Instance.SetGoldCountText(Current_GoldCount);
    }

    public void RevivePlayer()
    {
        Transform respawnPoint = lastRailEntered.transform.Find("RespawnPoint");
        if (respawnPoint != null)
        {
            PlayerStatusController.Instance.playerCurrentStatus = PlayerStatus.OffRail;
            Debug.Log("Set back to off rail");
            CartAnimationController.Instance.ResetCartAnimator();
            CharacterAnimationController.Instance.ResetCharacterAnimator();
            player.position = respawnPoint.position;
            player.rotation = lastRailEntered.transform.rotation;
            PlayerGravitySimulator.Instance.isFalling = true;
        }
        else
        {
            Debug.LogWarning("Respawn point not found. Cannot revive player.");
        }
    }

    public void ActiveGameObjAfterSecs(GameObject obj, float delay)
    {
        if (obj != null)
        {
            StartCoroutine(DelayActiveGameObject(obj, delay));
        }
        else
        {
            Debug.LogWarning("GameObject is null, cannot activate after delay.");
        }
    }

    private void SaveMePanelCheck()
    {
        if (PlayerStatusController.Instance.playerCurrentStatus == PlayerStatus.Dead                        // Player is dead
            && InGameController.Instance.saveMeBoxPopupCount <= 0                                           // No revive attempted yet in this game
            && InventoryManager.Instance.IsPurchased(ItemIDsContainers.Instance.itemID_SecondChance))       // Has second chance item in inventory             
        {
            if (saveMeUIBoxFreeAd.activeSelf == false)
            {
                saveMeUIBoxFreeAd.SetActive(true);
            }
            else
            {
                StartCoroutine(DelayActiveGameObject(gameOverPanel, 1f));
            }
            if(InGameController.Instance.saveMeBoxPopupCount < 0)
            {
                InGameController.Instance.saveMeBoxPopupCount = 1;
            }
            else
            {
                InGameController.Instance.saveMeBoxPopupCount++;
            }
        }
        else if (PlayerStatusController.Instance.playerCurrentStatus == PlayerStatus.Dead       // Player is dead
            && InGameController.Instance.saveMeBoxPopupCount >= 1                               // Revived once in this game
            && saveMeUIBoxFreeAd.activeSelf == false)                                           // Revive box is active
        {
            StartCoroutine(DelayActiveGameObject(gameOverPanel, 1f));
        }
    }

    IEnumerator DelayActiveGameObject(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
        yield break;
    }

}
