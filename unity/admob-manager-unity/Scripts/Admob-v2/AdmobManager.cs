using UnityEngine;
using System;
using UnityEngine.Events;

public class AdmobManager : MonoBehaviour
{
    public static AdmobManager Instance { get; private set; }
    private InterstitialAdController interstitialAdController;
    private RewardedAdController rewardedAdController;
    public UnityEvent boostCoinAdCollect;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            interstitialAdController = GetComponent<InterstitialAdController>();
            rewardedAdController = GetComponent<RewardedAdController>();
            DontDestroyOnLoad(this);
        }
    }
    private void Start()
    {
        // Interstitial Ad
        interstitialAdController.RequestAndLoadInterstitialAd();

        // Rewarded Ad
        rewardedAdController.RequestAndLoadRewardedAd();
    }

    public void ShowAd(AdmobType admobType)
    {
        if (admobType == AdmobType.InterstitialAd)
        {
            interstitialAdController.ShowAd();
        }
        if (admobType == AdmobType.RewardedAd)
        {
            rewardedAdController.ShowAd();
        }
    }

    public bool IsAdLoaded(AdmobType admobType)
    {
        if (admobType == AdmobType.InterstitialAd)
        {
            return interstitialAdController._isInterstitialAdLoaded;
        }
        if (admobType == AdmobType.RewardedAd)
        {
            return rewardedAdController._isRewardedAdLoaded;
        }
        return false;
    }

    public void GetBoostCoinAd(Action callback)
    {
        rewardedAdController.RegisterRewardedAdCustomHandlers(RewardedAdType.BoostCoin, callback);
    }
}

public enum AdmobType
{
    InterstitialAd,
    RewardedAd
}

public enum RewardedAdType
{
    BoostCoin,
}