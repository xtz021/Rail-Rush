using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuController : MonoBehaviour
{
    public static GameOverMenuController Instance;

    [SerializeField] Text passTicketCountText;
    [SerializeField] Text best_DistanceText;
    [SerializeField] Text best_GoldText;
    [SerializeField] Text current_DistanceText;
    [SerializeField] Text current_GoldText;

    private Coroutine distanceCoroutine;
    private Coroutine goldCoroutine;

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
        Debug.Log("" + gameObject.name);
    }

    private void OnEnable()
    {
        InGameController.Instance.SaveProgress();
        InventoryManager.Instance.SaveInventory();
        PlayerStatsUIHandler.SaveData(GameStatsController.Instance.playerStats);
        passTicketCountText.text = "" + InventoryManager.Instance.inventory.PassTickets;
        best_DistanceText.text = "" + InGameController.Instance.best_Distance;
        best_GoldText.text = "" + InGameController.Instance.best_Gold;
        //current_DistanceText.text = "" + InGameController.Instance.Current_DistanceCount;
        SetScoreTextAnimation(distanceCoroutine,current_DistanceText,InGameController.Instance.Current_DistanceCount);
        //current_GoldText.text = "" + InGameController.Instance.Current_GoldCount;
        SetScoreTextAnimation(goldCoroutine,current_GoldText,InGameController.Instance.Current_GoldCount);
    }

    private void SetScoreTextAnimation(Coroutine corr, Text scoreText, int score)
    {
        if(corr != null)
        {
            StopCoroutine(corr);
        }
        corr = StartCoroutine(ScoreGainUpAnimation(scoreText, score));
    }

    IEnumerator ScoreGainUpAnimation(Text scoreText, int score)
    {
        float timer = 0, duration = 2f;
        while(timer < duration)
        {
            if(timer + Time.deltaTime > duration)
            {
                timer = duration;
            }
            else
            {
                timer += Time.deltaTime;
            }
            int culmulatingScore = (int) (score * timer / duration);
            scoreText.text = "" + culmulatingScore;
            yield return null;
        }
        yield break;
    }
}
