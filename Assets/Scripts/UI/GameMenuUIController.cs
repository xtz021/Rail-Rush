using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuUIController : MonoBehaviour
{
    public static GameMenuUIController Instance { get; private set; }

    private const string SCENE_ID_MININGCAVE_STRING = "Scenes/MiningCave_Gameplay";

    [Header("UI objects in the main menu:")]
    [SerializeField] GameObject mainMenuButtons;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject gemsCollectionPanel;

    [Header("UI for GGPS:")]
    public Button scoreButton;
    public Text userNameText;
    public Button loginButton;

    private GameMenuUIController() { }

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
    }

    private void Start()
    {
        CheckOpenShopOnLoad();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SCENE_ID_MININGCAVE_STRING, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }


    public void OpenShop()
    {
        shopPanel.SetActive(true);
        optionPanel.SetActive(false);
        mainMenuButtons.SetActive(false);
        gemsCollectionPanel.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        optionPanel.SetActive(true);
        shopPanel.SetActive(false);
        mainMenuButtons.SetActive(false);
        gemsCollectionPanel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        mainMenuButtons.SetActive(true);
        optionPanel.SetActive(false);
        shopPanel.SetActive(false);
        gemsCollectionPanel.SetActive(false);
    }

    public void OpenGemsCollectionPanel()
    {
        gemsCollectionPanel.SetActive(true);
        shopPanel.SetActive(false);
        optionPanel.SetActive(false);
        mainMenuButtons.SetActive(false);
    }

    public void OpenWebInfo()
    {
        Application.OpenURL("https://miniclip.com/");
    }

    private void CheckOpenShopOnLoad()
    {
        if (GameStatsController.Instance.openShopFromGame)
        {
            GameStatsController.Instance.ResetOpenShop();
            OpenShop();
        }
    }
}
