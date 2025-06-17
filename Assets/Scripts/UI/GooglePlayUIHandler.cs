using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlayUIHandler : MonoBehaviour
{
    public static GooglePlayUIHandler Instance { get; private set; }

    [Header("Google Play Games UI Settings")]
    public Text UserNameText;
    [SerializeField] Button loginButton;
    public Button showAdsClaimNuggetButton;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if(GooglePlayGamesController.Instance == null)
        {
            Debug.LogError("GooglePlayGamesController instance is not initialized. Please ensure it is set up correctly.");
            return;
        }
        UpdatSignInUI();
    }

    private void UpdatSignInUI()
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
}
