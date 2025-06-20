using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameMenuController : MonoBehaviour
{
    [SerializeField] private Text bestScoreText;

    private void OnEnable()
    {
        bestScoreText.text = "" + GameStatsController.Instance.playerStats.BestRun;
    }

}
