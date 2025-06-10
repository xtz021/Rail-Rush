using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayGamesController : MonoBehaviour
{
    public static GooglePlayGamesController Instance { get; private set; }
    [Header("Google Play Games UI Settings")]
    public Text UserNameText;
    [SerializeField] Button loginButton;

    [Header("No need to assign these fields")]
    public string Token;
    public string Error;

    private bool isLoggedIn = false;

    void Awake()
    {
        // Singleton pattern to ensure only one instance of this controller exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Initialize PlayGamesPlatform
        PlayGamesPlatform.Activate();
        LoginGooglePlayGames();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }


    public void LoginGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }



    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services

            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string ImgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

            isLoggedIn = true;
            UserNameText.text = "" + name;
            loginButton.gameObject.SetActive(false);
        }
        else
        {
            UserNameText.text = "(Not Linked)";
            isLoggedIn = false;
            loginButton.gameObject.SetActive(true);
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
        }
    }


    public void ShowLeaderBoard()
    {
        if (isLoggedIn)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            LoginGooglePlayGames();
        }
    }

    public void PostLeaderBoardGoldScore(int _score)
    {
        Social.ReportScore(_score, GGPSids.LEADERBOARD_BESTDISTANCE,
            (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Distance HighScore" + _score + " submitted successfully!!!");
                }
                else
                {
                    Debug.Log("Failed to submit" + _score + " to Distance HighScore Leaderboard!!!");
                }
            });
    }

    public void PostLeaderBoardDistanceScore(int _score)
    {
        Social.ReportScore(_score, GGPSids.LEADERBOARD_MOSTGOLDEARN_INAGAME,
            (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Gold HighScore" + _score + " submitted successfully!!!");
                }
                else
                {
                    Debug.Log("Failed to submit" + _score + " to Gold HighScore Leaderboard!!!");
                }
            });
    }



}

public static class GGPSids
{
    public const string LEADERBOARD_BESTDISTANCE = "CgkIutam_aQNEAIQAQ";
    public const string LEADERBOARD_MOSTGOLDEARN_INAGAME = "CgkIutam_aQNEAIQAg";
}