using UnityEngine;
using System;
using UnityEngine.Events;

public class AdmobManager : MonoBehaviour
{
    public static AdmobManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Interstitial Ad
        InterstitialAdController.RequestAndLoadInterstitialAd();

        // Rewarded Ad
        RewardedAdController.RequestAndLoadRewardedAd();
    }

    public void UseInterstitialAd() {
        InterstitialAdController.ShowAd();
    }

    public void UseRewardedAd(RewardedAdController.OnRewardedAdHandle method)
    {
        RewardedAdController.ShowAd();
        RewardedAdController.rewardedAdEvent += method;
    }
}