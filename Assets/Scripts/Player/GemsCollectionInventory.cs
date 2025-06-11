using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsCollectionInventory : MonoBehaviour
{
    public static GemsCollectionInventory Instance {  get; private set; }

    public List<GemCollectedData> gemDatas;



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
        LoadCollectionData();
    }

    private void OnDisable()
    {
        SaveCollectionData();
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

