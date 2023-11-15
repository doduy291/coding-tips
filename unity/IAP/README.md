### Setup

**Official**: https://docs.unity3d.com/Packages/com.unity.purchasing@4.8/manual/Overview.html <br>
**Video Tutorial**: https://www.youtube.com/watch?v=PthFk5je350&ab_channel=Coco3D (I used to follow this tutorial)

### Caution

Do not forget to use `DontDestroyOnLoad` for IAPManager. In some cases, IAP does not work when scene is reloaded, so make sure IAP only Initialize IAP once at app start and use a singleton pattern.

```js

# IAPManager.cs

private void Awake()
{
    if (Instance != null && Instance != this) {
        Destroy(this);
    }
    else {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
```
