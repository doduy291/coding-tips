using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AdmobShop : MonoBehaviour
{
    [SerializeField] AdmobController admobController;
    [SerializeField] Button freeAdsCoinButton;
    [SerializeField] Button freeAdsDiamondButton;

    private void Awake()
    {
        admobController.RequestAndLoadRewardedAd();
    }

    private void Start()
    {
        freeAdsCoinButton.onClick.AddListener(watchAdsToReceiveCurrency);
        freeAdsDiamondButton.onClick.AddListener(watchAdsToReceiveCurrency);
    }

    private void watchAdsToReceiveCurrency()
    {
        admobController.ShowRewardedAd();
    }

}
