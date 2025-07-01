using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    int gemValue = 0;
    string gemName;
    AudioSource audioSource;

    private void Start()
    {
        GetGemValue();
        //audioSource = GetComponent<AudioSource>();
    }

    private void GetGemValue()
    {
        foreach(Gem gem in GemValueHandler.Instance.GetGemList())
        {
            if(gameObject.name.Contains(gem.Name))
            {
                gemValue = gem.Value;
                gemName = gem.Name;
                break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InGameController.Instance.GainGold(gemValue);
            UpdatePlayerGemStats(gemName, GameStatsController.Instance.playerStats);
            //if (audioSource != null)
            //{
            //    audioSource.Play();
            //}
            AudioManager.Instance.Play("Gem");
            MissionsManager.Instance.UpdateMissionProgressByType(MissionType.CollectGems);
            Destroy(gameObject);
        }
    }

    private void UpdatePlayerGemStats(string gemName, PlayerStats playerStats)
    {
        playerStats.TotalGemsCollected++;
        switch (gemName)
        {
            case "Amethyst":
                playerStats.AmethystCollected++;
                break;
            case "Garnet":
                playerStats.GarnetCollected++;
                break;
            case "Topaz":
                playerStats.TopazCollected++;
                break;
            case "Spinel":
                playerStats.SpinelCollected++;
                break;
            case "Ruby":
                playerStats.RubyCollected++;
                break;
            case "Sapphire":
                playerStats.SapphireCollected++;
                break;
            case "Emerald":
                playerStats.EmeraldCollected++;
                break;
            case "Diamond":
                playerStats.DiamondCollected++;
                break;
            default:
                Debug.LogWarning("Unknown gem type: " + gemName);
                break;
        }
    }

}
