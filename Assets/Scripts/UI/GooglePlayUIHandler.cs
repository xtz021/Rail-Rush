using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlayUIHandler : MonoBehaviour
{
    public static GooglePlayUIHandler Instance { get; private set; }

    [Header("Google Play Games UI Settings")]
    public Text UserNameText;
    public Button loginButton;
    public Button showAdsClaimNuggetButton;
    public Button leaderBoardButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if(GooglePlayGamesController.Instance == null)
        {
            Debug.LogError("GooglePlayGamesController instance is not initialized. Please ensure it is set up correctly.");
            return;
        }
        UpdateSignInUI();
        AddShowLeaderBoardFunction();
    }

    public void UpdateSignInUI()
    {
        if (GooglePlayGamesController.Instance.isLoggedIn)
        {
            UserNameText.text = GooglePlayGamesController.Instance.UserName;
            loginButton.gameObject.SetActive(false);
        }
        else
        {
            UserNameText.text = "(Not Linked)";
            loginButton.gameObject.SetActive(true);
        }
    }

    private void AddShowLeaderBoardFunction()
    {
        if (leaderBoardButton != null)
        {
            leaderBoardButton.onClick.RemoveAllListeners();
            leaderBoardButton.onClick.AddListener(() => GooglePlayGamesController.Instance.ShowLeaderBoard());
        }
        else
        {
            Debug.LogWarning("LeaderBoard button is not assigned in the GooglePlayUIHandler.");
        }
    }

}
