using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    public static InGameUIController Instance { get; private set; }
    public GameObject PauseMenuPanel;
    public Text goldCountText;

    private const string SCENE_ID_OPENINGMENU_STRING = "Scenes/OpeningMenu";

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetGoldCountText(int gold)
    {
        goldCountText.text = "" + gold;
    }

    public void PauseGame()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(SCENE_ID_OPENINGMENU_STRING, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    public void OpenShopFromInGameMenu()
    {
        GameStatsController.Instance.OpenShopFromGame();
        OpenMainMenu();
    }
}
