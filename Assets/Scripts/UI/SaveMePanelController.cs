using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMePanelController : MonoBehaviour
{
    [SerializeField] private Image TimeOutFiller;
    [SerializeField] private GameObject gameOverPanel;

    private float timeOutDuration = 3f; // Duration in seconds
    private bool playerIsSaved = false;

    private void OnEnable()
    {
        playerIsSaved = false;
        TimeOutFiller.fillAmount = 1;
        StartCoroutine(RunOutTimerSaveMePanel());
    }


    IEnumerator RunOutTimerSaveMePanel()
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            TimeOutFiller.fillAmount = 1 - (elapsedTime / timeOutDuration);
            if (playerIsSaved)
            {
                // Player clicked to revive the character, so we stop the corroutine to prevent the gameOverPanel from being enabled
                gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }
        // Gameover if player doesn't click to revive the character
        InGameController.Instance.ActiveGameObjAfterSecs(gameOverPanel, 0.5f);
        gameObject.SetActive(false);
        yield break;
    }

    public void OnClickRevive()
    {
        if (InventoryManager.Instance.UseConsumable(ItemIDsContainers.Instance.itemID_SecondChance))
        {
            InGameController.Instance.RevivePlayer();
            playerIsSaved = true;
        }
    }

}
