using Newtonsoft.Json;
using System;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class UsageDemo : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("RemoteConfig::Cached:: " + RemoteConfigService.Instance.GetConfig("settings").GetBool("is_Active"));
        Debug.Log("RemoteConfig:: " + RemoteConfigManager.GetBool("is_Active"));

        Debug.Log("RemoteConfig:: " + RemoteConfigManager.GetJson("shop-data").ToString());

        ShopData[] dataTest = JsonConvert.DeserializeObject<ShopData[]>(RemoteConfigManager.GetJson("shop-data-array"));
        foreach (ShopData shopData in dataTest)
        {
            Debug.Log(shopData.nameReward);
        }
    }
}

[Serializable]
public class ShopData
{
    public string nameReward;
    public int id;
}
