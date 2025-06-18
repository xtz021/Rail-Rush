using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollectionUI : MonoBehaviour
{
    [SerializeField] GameObject amethyst;
    [SerializeField] GameObject garnet;
    [SerializeField] GameObject topaz;
    [SerializeField] GameObject spinel;
    [SerializeField] GameObject ruby;
    [SerializeField] GameObject sapphire;
    [SerializeField] GameObject emerald;
    [SerializeField] GameObject diamond;

    private void OnEnable()
    {
        CheckCollectionStatus();
    }


    public void CheckCollectionStatus()
    {
        PlayerStats playerStats = GameStatsController.Instance.playerStats;
        if (playerStats != null)
        {
            if(playerStats.AmethystCollected > 0) 
                amethyst.SetActive(true);
            if (playerStats.GarnetCollected > 0)
                garnet.SetActive(true);
            if (playerStats.TopazCollected > 0)
                topaz.SetActive(true);
            if(playerStats.SpinelCollected > 0)
                spinel.SetActive(true);
            if(playerStats.RubyCollected > 0)
                ruby.SetActive(true);
            if(playerStats.SapphireCollected > 0)
                sapphire.SetActive(true);
            if(playerStats.EmeraldCollected > 0)
                emerald.SetActive(true);
            if(playerStats.DiamondCollected > 0)
                diamond.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Unable to load player stats to get gem collection data.");
        }
    }
}
