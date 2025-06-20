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
    [SerializeField] GameObject missionsPanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject gemsCollectionPanel;
    [SerializeField] GameObject statsPanel;

    [Header("UI for GGPS:")]
    public Button scoreButton;
    public Text userNameText;
    public Button loginButton;

    [Header("PopUp Notice components:")]
    [SerializeField] GameObject noticePopUpPrefab;
    [SerializeField] Transform canvasParent;

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
        missionsPanel.SetActive(false);
        optionPanel.SetActive(false);
        mainMenuButtons.SetActive(false);
        gemsCollectionPanel.SetActive(false);
        statsPanel.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        optionPanel.SetActive(true);
        missionsPanel.SetActive(false);
        shopPanel.SetActive(false);
        mainMenuButtons.SetActive(false);
        gemsCollectionPanel.SetActive(false);
        statsPanel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        mainMenuButtons.SetActive(true);
        missionsPanel.SetActive(false);
        optionPanel.SetActive(false);
        shopPanel.SetActive(false);
        gemsCollectionPanel.SetActive(false);
        statsPanel.SetActive(false);
    }

    public void OpenGemsCollectionPanel()
    {
        gemsCollectionPanel.SetActive(true);
        missionsPanel.SetActive(false);
        shopPanel.SetActive(false);
        optionPanel.SetActive(false);
        mainMenuButtons.SetActive(false);
        statsPanel.SetActive(false);
    }

    public void OpenStatsPanel()
    {
        statsPanel.SetActive(true);
        shopPanel.SetActive(false);
        optionPanel.SetActive(false);
        mainMenuButtons.SetActive(false);
        missionsPanel.SetActive(false);
        gemsCollectionPanel.SetActive(false);
    }

    public void OpenMissionsPanel()
    {
        missionsPanel.SetActive(true);
        shopPanel.SetActive(false);
        optionPanel.SetActive(false);
        mainMenuButtons.SetActive(false);
        gemsCollectionPanel.SetActive(false);
        statsPanel.SetActive(false);
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

    public void PopUpNotice(string noticeText)
    {
        GameObject noticePopUp = Instantiate(noticePopUpPrefab, canvasParent);
        NoticePopUpController noticeController = noticePopUp.GetComponent<NoticePopUpController>();
        if (noticeController != null)
        {
            noticeController.SetNoticeText(noticeText);
        }
        else
        {
            Debug.LogWarning("NoticePopUpController component not found on the instantiated prefab.");
        }
    }

}
