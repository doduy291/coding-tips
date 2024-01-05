### Check App Update

1. Compare versions

```csharp
private bool VersionCompare(string currentVer, string newVer)
    {
        // part of version
        int vnum1 = 0, vnum2 = 0;

        for (int i = 0, j = 0; (i < currentVer.Length
                                || j < newVer.Length);)
        {
            while (i < currentVer.Length && currentVer[i] != '.')
            {
                vnum1 = vnum1 * 10 + (currentVer[i] - '0');
                i++;
            }

            while (j < newVer.Length && newVer[j] != '.')
            {
                vnum2 = vnum2 * 10 + (newVer[j] - '0');
                j++;
            }

            if (vnum1 > vnum2)
                return true;
            if (vnum2 > vnum1)
                return false;

            // if equal, reset variables and
            // go for next numeric part
            vnum1 = vnum2 = 0;
            i++;
            j++;
        }
        return true;
    }
```

2. Move to Google Play store

```csharp
private void DirectlyOpen() {
    Application.OpenURL($"https://play.google.com/store/apps/details?id={Application.identifier}");
}
```

### Alternative method (Optional)

You can update app using In-App Update SDK provided by Google on the Android platform
<br>
https://developer.android.com/guide/playcore/in-app-updates/unity
