using System;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

[AddComponentMenu("GoogleMobileAds/Samples/GoogleMobileAdsController")]
public class GoogleAdmobController : MonoBehaviour
{
    // Always use test ads.
    // https://developers.google.com/admob/unity/test-ads
    //    internal static List<string> TestDeviceIds = new List<string>()
    //        {
    //            AdRequest.TestDeviceSimulator,
    //#if UNITY_IPHONE
    //            "96e23e80653bb28980d3f40beb58915c",
    //#elif UNITY_ANDROID
    //            "702815ACFC14FF222DA1DC767672A573"
    //#endif
    //        };

    // The Google Mobile Ads Unity plugin needs to be run only once.
    private static bool? _isInitialized;

    private void Start()
    {
        // On Android, Unity is paused when displaying interstitial or rewarded video.
        // This setting makes iOS behave consistently with Android.
        MobileAds.SetiOSAppPauseOnBackground(true);

        // When true all events raised by GoogleMobileAds will be raised
        // on the Unity main thread. The default value is false.
        // https://developers.google.com/admob/unity/quick-start#raise_ad_events_on_the_unity_main_thread
        MobileAds.RaiseAdEventsOnUnityMainThread = true;

        // Configure your RequestConfiguration with Child Directed Treatment
        // and the Test Device Ids.
        //MobileAds.SetRequestConfiguration(new RequestConfiguration
        //{
        //    TestDeviceIds = TestDeviceIds
        //});

        InitializeGoogleMobileAds();
    }

    /// <summary>
    /// Initializes the Google Mobile Ads Unity plugin.
    /// </summary>
    private void InitializeGoogleMobileAds()
    {
        // The Google Mobile Ads Unity plugin needs to be run only once and before loading any ads.
        if (_isInitialized.HasValue)
        {
            return;
        }

        _isInitialized = false;

        // Initialize the Google Mobile Ads Unity plugin.
        Debug.Log("Google Mobile Ads Initializing.");
        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            if (initstatus == null)
            {
                Debug.LogError("Google Mobile Ads initialization failed.");
                _isInitialized = null;
                return;
            }

            Debug.Log("Google Mobile Ads initialization complete.");
            _isInitialized = true;
        });
    }

    /// <summary>
    /// Opens the AdInspector.
    /// </summary>
    public void OpenAdInspector()
    {
        Debug.Log("Opening ad Inspector.");
        MobileAds.OpenAdInspector((AdInspectorError error) =>
        {
            // If the operation failed, an error is returned.
            if (error != null)
            {
                Debug.Log("Ad Inspector failed to open with error: " + error);
                return;
            }

            Debug.Log("Ad Inspector opened successfully.");
        });
    }
}
