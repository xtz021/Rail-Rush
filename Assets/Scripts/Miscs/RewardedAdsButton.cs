using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    string _androidAdUnitId;
    string _iOSAdUnitId;
    string _adUnitId = null; // This will remain null for unsupported platforms

    //[SerializeField] GameOverHandler handler;

    private void Awake()
    {
        _androidAdUnitId = "Rewarded_Android";
        _iOSAdUnitId = "Rewarded_iOS";
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#elif UNITY_EDITOR
        _adUnitId = _androidAdUnitId;
#endif
    }

    void Start()
    {
        // Disable the button until the ad is ready to show:
        if(_showAdButton != null)
        {
            _showAdButton.interactable = true;
        }
    }

    private void OnEnable()
    {
        LoadAdForAdsButton(); // Load the ad when the script is enabled
    }
    private void OnDisable()
    {
        if (_showAdButton != null)
        {
            _showAdButton.onClick.RemoveAllListeners(); // Remove listeners to prevent memory leaks
        }
    }

    private void LoadAdForAdsButton()
    {
        if (GooglePlayUIHandler.Instance != null && GooglePlayUIHandler.Instance.showAdsClaimNuggetButton != null)
        {
            _showAdButton = GooglePlayUIHandler.Instance.showAdsClaimNuggetButton;     // Assign the button from GooglePlayUIHandler
            _showAdButton.onClick.RemoveAllListeners(); // Clear any existing listeners to avoid duplicates
            _showAdButton.onClick.AddListener(ShowAd);
            _showAdButton.interactable = true;
        }
        else
        {
            Debug.LogWarning("GooglePlayUIHandler or showAdsClaimNuggetButton is not initialized.");
        }
    }


    public void AssignUI(Button showAdButton)
    {
        _showAdButton = showAdButton;
    }

    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }


    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Disable the button:
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
        //handler.StopAllCoroutines();
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            int rewardAmount = 300 * InventoryManager.Instance.NuggetBonusMultiplier; // Calculate the reward amount
            InventoryManager.Instance.inventory.GainGold(rewardAmount); // Add 300 gold as a reward
            GameMenuUIController.Instance.PopUpNotice($"You have received {rewardAmount} gold as a reward!"); // Show a notice pop-up
            //handler.DoubleReward();
            //DailyArchivementHandler.adsWatchedOfDay++;
            AdsInitializer.Instance.NewRewardedAdsInstance(); // Load a new ad instance after showing the ad
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
        AdsInitializer.Instance.NewRewardedAdsInstance(); // Load a new ad instance after failure
        //handler.ShowRewardUI();
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
        AdsInitializer.Instance.NewRewardedAdsInstance(); // Load a new ad instance after failure
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}