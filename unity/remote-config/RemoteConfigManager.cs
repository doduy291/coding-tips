using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using System.Threading.Tasks;

public class RemoteConfigManager : MonoBehaviour
{
    public static RemoteConfigManager Instance { get; private set; }
    public struct userAttributes { }
    public struct appAttributes { }
    public struct filterAttributes
    {
        // Optionally declare variables for attributes to filter on any of following parameters:
        public string[] key;
    }

    private string enviromentId = "xxxxx-xxx-xxxx-xxxx-ab265bfcdbea";

    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    async Task Start()
    {
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        // Attribute Configs
        // Select the keys that need to get to prevent fetching the whole data
        var fAttributes = new filterAttributes();
        fAttributes.key = new string[] { "shop-data", "is_Active" };

        // Configs
        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.SetEnvironmentID(enviromentId);
        await RemoteConfigService.Instance.FetchConfigsAsync(new userAttributes(), new appAttributes(), fAttributes);
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("RemoteConfig:: No settings loaded this session and no local cache file exists; using default values.");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("RemoteConfig:: No settings loaded this session; using cached values from a previous session.");
                break;
            case ConfigOrigin.Remote:
                Debug.Log("RemoteConfig:: New settings loaded this session; update values accordingly.");
                break;
        }
        Debug.Log("RemoteConfig:: RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());
    }

    // Other types (int, float,...) is same
    public static bool GetBool(string key, bool defaultValue = false)
    {
        return RemoteConfigService.Instance.appConfig.GetBool(key, defaultValue);
    }

    public static string GetJson(string key, string defaultValue = "{}")
    {
        return RemoteConfigService.Instance.appConfig.GetJson(key, defaultValue);
    }

    public static string CheckNewVersion(string key)
    {
        RemoteConfigService.Instance.FetchConfigsAsync(new userAttributes(), new appAttributes());
        return RemoteConfigService.Instance.appConfig.GetString(key);
    }
}
