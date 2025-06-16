using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsDataHandler : MonoBehaviour
{
    [Header("Stats UI Text")]
    [SerializeField] TMP_Text tmp_BestRun;
    [SerializeField] TMP_Text tmp_NuggetsCollected;
    [SerializeField] TMP_Text tmp_MaxNuggetsCollectedInAGame;
    [SerializeField] TMP_Text tmp_TotalPlays;
    [SerializeField] TMP_Text tmp_TotalMissionsCompleted;
    [Space(20f)]
    [SerializeField] TMP_Text tmp_Deaths;
    [SerializeField] TMP_Text tmp_DeathsByUpperObs;
    [SerializeField] TMP_Text tmp_DeathsByLowerObs;
    [SerializeField] TMP_Text tmp_DeathsByRightObs;
    [SerializeField] TMP_Text tmp_DeathsByleftObs;
    [Space(20f)]
    [SerializeField] TMP_Text tmp_AmethystCollected;
    [SerializeField] TMP_Text tmp_GarnetCollected;
    [SerializeField] TMP_Text tmp_TopazCollected;
    [SerializeField] TMP_Text tmp_SpinelCollected;
    [SerializeField] TMP_Text tmp_RubyCollected;
    [SerializeField] TMP_Text tmp_SapphireCollected;
    [SerializeField] TMP_Text tmp_EmeraldCollected;
    [SerializeField] TMP_Text tmp_DiamondCollected;
    [SerializeField] TMP_Text tmp_TotalGemsCollected;
}

public class PlayerStats
{
    int BestRun;
    int NuggetsCollected;

}