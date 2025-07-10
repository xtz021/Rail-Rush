using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    int gemValue = 0;
    string gemName;
    [SerializeField] GemType gemType = GemType.Amethyst;
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
            if(gemType == gem.gemType)
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
        playerStats.GemCollectedByTypes[gemType]++;
    }

}

public enum GemType
{
    Amethyst,
    Garnet,
    Topaz,
    Spinel,
    Ruby,
    Sapphire,
    Emerald,
    Diamond
}
