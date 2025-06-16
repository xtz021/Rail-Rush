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
        audioSource = GetComponent<AudioSource>();
    }

    private void GetGemValue()
    {
        foreach(Gem gem in GemValueHandler.Instance.GetGemList())
        {
            if(gameObject.name.Contains(gem.Name))
            {
                gemValue = gem.Value;
                gemName = gem.Name;
                UpdatePlayerStats(gem.Name);
                break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InGameController.Instance.GainGold(gemValue);
            if (audioSource != null)
            {
                audioSource.Play();
            }
            Destroy(gameObject);
        }
    }

    private void UpdatePlayerStats(string gemName)
    {
        PlayerStatsDataHandler.playerStats.TotalGemsCollected++;
        switch (gemName)
        {
            case "Amethyst":
                PlayerStatsDataHandler.playerStats.AmethystCollected++;
                break;
            case "Garnet":
                PlayerStatsDataHandler.playerStats.GarnetCollected++;
                break;
            case "Topaz":
                PlayerStatsDataHandler.playerStats.TopazCollected++;
                break;
            case "Spinel":
                PlayerStatsDataHandler.playerStats.SpinelCollected++;
                break;
            case "Ruby":
                PlayerStatsDataHandler.playerStats.RubyCollected++;
                break;
            case "Sapphire":
                PlayerStatsDataHandler.playerStats.SapphireCollected++;
                break;
            case "Emerald":
                PlayerStatsDataHandler.playerStats.EmeraldCollected++;
                break;
            case "Diamond":
                PlayerStatsDataHandler.playerStats.DiamondCollected++;
                break;
            default:
                Debug.LogWarning("Unknown gem type: " + gemName);
                break;
        }
    }

}
