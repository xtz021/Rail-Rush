using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsCollectionInventory : MonoBehaviour
{
    public static GemsCollectionInventory Instance {  get; private set; }

    public GemsDataSO gemDataSO; // Reference to the ScriptableObject data containing names of the gems

    [HideInInspector] public List<GemCollectedData> gemDatas; // List to hold gem data

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (gemDataSO != null)
        {
            GetGemNamesData();
        }
        else
        {
            Debug.LogError("GemsDataSO is not assigned in GemsCollectionInventory.");
        }
        LoadCollectionData();       // Load previously saved gem collection data
    }

    private void OnDisable()
    {
        SaveCollectionData();
    }

    private void GetGemNamesData()
    {
        if (gemDataSO != null)
        {
            for (int i = 0; i < gemDataSO.gemDatas.Count; i++)
            {
                GemCollectedData gemData = new GemCollectedData
                {
                    gemName = gemDataSO.gemDatas[i].Name,
                    collectedCount = 0 // Initialize collected count to 0
                };
                gemDatas.Add(gemData);
            }
        }
        else
        {
            Debug.LogError("GemsDataSO is not assigned in GemsCollectionInventory.");
        }
    }

    public void SaveCollectionData()
    {
        foreach (var gem in gemDatas)
        {
            string gemID = "Collected_" + gem.gemName;
            PlayerPrefs.SetInt(gemID,gem.collectedCount);
        }
    }

    private void LoadCollectionData()
    {
        for(int i = 0; i < gemDatas.Count;i++)
        {
            string gemID = "Collected_" + gemDatas[i].gemName;
            gemDatas[i].collectedCount = PlayerPrefs.GetInt(gemID);
        }
    }
}

[Serializable]
public class GemCollectedData
{
    public string gemName;
    public int collectedCount;
}

