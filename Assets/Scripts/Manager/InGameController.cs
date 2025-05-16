using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    public static InGameController Instance { get; private set; }

    public static int GoldCount;
    public static int DistanceCount;

    [Header("Variables")]
    public int saveMeBoxPopupCount = 0;

    [Header("Do not need input")]
    public GameObject lastRailEntered;

    [Header("Need input")]
    [SerializeField] private GameObject saveMeUIBoxFreeAd;
    [SerializeField] private GameObject gameOverPanel;

    private int totalGold = 0;
    private int best_Distance = 0;
    private bool isProgressSaved = false;
    private const string KEY_TOTALGOLD = "_TotalGold";
    private const string KEY_BESTDISTANT = "_Best_Distant";
    private Transform player;

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

    public void GainGold(int gold)
    {
        GoldCount += gold;
        InGameUIController.Instance.SetGoldCountText(GoldCount);
    }

    public void RevivePlayer()
    {
        Transform respawnPoint = lastRailEntered.transform.Find("RespawnPoint");
        if (respawnPoint != null)
        {
            player.position = respawnPoint.position;
            player.rotation = lastRailEntered.transform.rotation;
            PlayerStatusController.Instance.playerCurrentStatus = PlayerStatus.OffRail;
            PlayerGravitySimulator.Instance.isFalling = true;
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
        if (PlayerStatusController.Instance.playerCurrentStatus == PlayerStatus.Dead     // Player is dead
            && InGameController.Instance.saveMeBoxPopupCount <= 0)                      // No revive attempted yet in this game
        {
            if (saveMeUIBoxFreeAd.activeSelf == false)
            {
                saveMeUIBoxFreeAd.SetActive(true);
                InGameController.Instance.saveMeBoxPopupCount++;
            }
        }
        else if (PlayerStatusController.Instance.playerCurrentStatus == PlayerStatus.Dead     // Player is dead
            && InGameController.Instance.saveMeBoxPopupCount >= 1                           // Revived once in this game
            && saveMeUIBoxFreeAd.activeSelf == false)                                          // Revive box is active
        {

        }
    }

    IEnumerator DelayActiveGameObject(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }

}
