using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdmobDailyReward : MonoBehaviour
{
    [SerializeField] AdmobController admobController;

    private void Awake()
    {
        admobController.RequestAndLoadRewardedAd();
    }

    private void Start()
    {

    }

    public void watchAdsToReceiveOneTimeAttendance()
    {
        admobController.ShowRewardedAd();
    }

}
