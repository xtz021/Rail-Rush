using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemValueHandler : MonoBehaviour
{
    public static GemValueHandler Instance { get; private set; }

    [Header("Gem Value Data")]
    [SerializeField] private GemsDataSO gemsDataSO; // ScriptableObject containing gem data

    [Header("Data to check gems value")]

    [Header("Gems to spawn")]
    public List<GameObject> commonGems;
    public List<GameObject> rareGems;
    public List<GameObject> jackPotGems;

    [HideInInspector] private List<Gem> GemList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            GemList = gemsDataSO.gemDatas; // Initialize the gem list from the ScriptableObject
        }
    }


    public List<Gem> GetGemList()
    {
        return GemList;
    }


}
