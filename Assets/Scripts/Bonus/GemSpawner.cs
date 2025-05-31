using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    List<GameObject> gemPrefabList;
    Rarity rarity;
    int spawnRate = 100;                 // Gem has 10% spawn rate (spawnRate/totalWeight * 100)
    int totalWeight = 1000;          // Total weight for gacha system
    int jackpotRange = 50;       // Range rate for Jackpot rarity (5%)
    int rareRange = 250;          // Range rate for Rare rarity (20%) rate = (rareRange - jackpotRange) / totalWeight * 100

    private void Start()
    {
        if(CheckIfGemSpawn())
        {
            rarity = GetGemRarity();
            if (rarity == Rarity.Jackpot)
            {
                gemPrefabList = GemValueHandler.Instance.jackPotGems;
            }
            else if (rarity == Rarity.Rare)
            {
                gemPrefabList = GemValueHandler.Instance.rareGems;
            }
            else
            {
                gemPrefabList = GemValueHandler.Instance.commonGems;
            }
            SpawnGem();
        }
    }

    private Rarity GetGemRarity()
    {
        int gacha = Random.Range(0, totalWeight);
        if(gacha >= 0 && gacha < jackpotRange)         // Rate 5% return JackPot
        {
            return Rarity.Jackpot;
        }
        else if(gacha >= jackpotRange && gacha < rareRange)   // Rate 20% return Rare
        {
            return Rarity.Rare;
        }
        else                                // Rate 75% return Common
        {
            return Rarity.Common;
        }
    }

    private void SpawnGem()
    {
        int index = Random.Range(0, gemPrefabList.Count);
        Instantiate(gemPrefabList[index],transform.position,transform.rotation,transform.parent);
    }

    private bool CheckIfGemSpawn()
    {
        int gacha = Random.Range(0, totalWeight);
        if(gacha < spawnRate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public enum Rarity
{
    Common,
    Rare,
    Jackpot
}
