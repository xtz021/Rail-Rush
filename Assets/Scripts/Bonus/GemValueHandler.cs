using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemValueHandler : MonoBehaviour
{
    public static GemValueHandler Instance { get; private set; }

    public const int AmethystValue = 10;
    public const int GarnetValue = 15;
    public const int TopazValue = 20;
    public const int SpinelValue = 50;
    public const int RubyValue = 100;
    public const int SapphireValue = 500;
    public const int EmeraldValue = 1000;
    public const int DiamondValue = 5000;

    [Serializable]
    public struct Gem
    {
        public string Name;
        public int Value;
    }

    [Header("Data to check gems value")]
    [SerializeField] private List<Gem> GemList;

    [Header("Gems to spawn")]
    public List<GameObject> commonGems;
    public List<GameObject> rareGems;
    public List<GameObject> jackPotGems;

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

    public List<Gem> GetGemList()
    {
        return GemList;
    }


}
