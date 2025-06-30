using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    public static AdsInitializer Instance { get; private set; }

    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    public RewardedAdsButton rewarded_ads_instance;
    [SerializeField] GameObject rewarded_ads_prefab;
    [SerializeField] Button _showAdButton;

    void Awake()
    { 
        if (Instance != null && Instance != this)
        { 
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        NewRewardedAdsInstance();
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void NewRewardedAdsInstance()
    {
        if (rewarded_ads_instance != null)
        {
            var oldRewarded = FindObjectOfType<RewardedAdsButton>();
            Destroy(oldRewarded.gameObject);
        }
        var newRewarded = Instantiate(rewarded_ads_prefab, Vector3.zero, Quaternion.identity);
        rewarded_ads_instance = newRewarded.GetComponent<RewardedAdsButton>();
        rewarded_ads_instance.AssignUI(GooglePlayUIHandler.Instance.showAdsClaimNuggetButton);
        StartCoroutine(DelayLoadRewardAds(rewarded_ads_instance));
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        rewarded_ads_instance.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");

    }

    private IEnumerator DelayLoadRewardAds(RewardedAdsButton rewardedAds)
    {
        yield return new WaitForSeconds(.2f);
        rewardedAds.LoadAd();
        yield break;
    }
}