using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayGamesController : MonoBehaviour
{
    public Text UserNameText;

    public string Token;
    public string Error;

    private bool isLoggedIn = false;

    void Awake()
    {
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
        //PlayGamesPlatform.Instance.Authenticate((success) =>
        //{
        //    if (success == SignInStatus.Success)
        //    {
        //        Debug.Log("Login with Google Play games successful.");

        //        PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
        //        {
        //            Debug.Log("Authorization code: " + code);
        //            Token = code;
        //            // This token serves as an example to be used for SignInWithGooglePlayGames
        //        });
        //        SyncStatusButton.text = "Linked";
        //        isLoggedIn = true;
        //    }
        //    else
        //    {
        //        isLoggedIn = false;
        //        Error = "Failed to retrieve Google play games authorization code";
        //        Debug.Log("Login Unsuccessful");
        //        SyncStatusButton.text = "Not Linked";
        //        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
        //    }
        //});
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

        }
        else
        {
            UserNameText.text = "(Not Linked)";
            isLoggedIn = false;
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

    public void PostLeaderBoardScore(int _score)
    {
        Social.ReportScore(_score, GGPSids.LEADERBOARD_HIGHSCORE,
            (bool success) =>
            {
                if (success)
                {
                    Debug.Log("HighScore" + _score + " submitted successfully!!!");
                }
                else
                {
                    Debug.Log("Failed to submit" + _score + " to HighScore Leaderboard!!!");
                }
            });
    }



}

public static class GGPSids
{
    public const string LEADERBOARD_HIGHSCORE = "CgkI0oHbk_4KEAIQAQ";
}