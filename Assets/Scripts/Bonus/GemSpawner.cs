using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    List<GameObject> gemPrefabList;
    Rarity rarity;
    int spawnRate = 20;                 // Gem has 20% spawn rate

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
        int gacha = Random.Range(0, 100);
        if(gacha >= 0 && gacha < 5)         // Rate 5% return JackPot
        {
            return Rarity.Jackpot;
        }
        else if(gacha >= 5 && gacha < 25)   // Rate 20% return Rare
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
        int gacha = Random.Range(0, 100);
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
