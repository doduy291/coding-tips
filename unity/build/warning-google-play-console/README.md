# Fix warnings on Google Play Console built in **Unity**

- My Version: `Unity 2021.3.12f`

# Contents

### The developer of androidx.fragment:fragment (androidx.fragment:fragment) has reported version 1.0.0 as outdated. Consider upgrading to one of the following versions before publishing a new release: 1.1.0+.

1. Open **Project Settings** > **Player** > Android Tab > **Publishing Settings** > Enable **Custom Main Gradle Template**.
2. Open your gradle file at path `<PROJECT_FOLDER>\Assets\Plugins\Android\mainTemplate.gradle` and supplement newer version (For example: mine is `1.2.5`).

```js
dependencies {
    ...
    implementation 'androidx.fragment:fragment:1.2.5'
**DEPS**}
```

### There is no deobfuscation file associated with this App Bundle. If you use obfuscated code (R8/proguard), uploading a deobfuscation file will make crashes and ANRs easier to analyze and debug. Using R8/proguard can help reduce app size.

1. Open **Project Settings** > **Player** > **Android** Tab > **Publishing Settings** > Enable **Custom Launcher Gradle Template**, **Use R8** and **Release**.
2. Open your gradle file at path `<PROJECT_FOLDER>\Assets\Plugins\Android\launcherTemplate.gradle` and supplement like this:

<br>
**Reference**: https://developer.android.com/build/shrink-code#shrink-resources

```js
android {
    ...
    buildTypes {
        release {
            // MINIFY_RELEASE is based on Release in Step 1
            minifyEnabled **MINIFY_RELEASE** // <== Important
            shrinkResources true // <== Important
            proguardFiles getDefaultProguardFile('proguard-android.txt')**SIGNCONFIG**
        }
    }
}
```

3. ⚠️ In case minifying **Release** mode may make `Google Admob` or other ads disabled in Build.

- Back to **Project Settings** > **Player** > **Android** Tab > **Publishing Settings** > Enable **Custom Proguard File**
- Open your proguard file at path `<PROJECT_FOLDER>\Assets\Plugins\Android\proguard-user.txt` and supplement like this:

Reason:

```js
# For Google Play Services
-keep public class com.google.android.gms.ads.**{
   public *;
}

# For old ads classes
-keep public class com.google.ads.**{
   public *;
}

# Other required classes for Google Play Services
# Read more at http://developer.android.com/google/play-services/setup.html
-keep class * extends java.util.ListResourceBundle {
   protected Object[][] getContents();
}

-keep public class com.google.android.gms.common.internal.safeparcel.SafeParcelable {
   public static final *** NULL;
}

-keepnames @com.google.android.gms.common.annotation.KeepName class *
-keepclassmembernames class * {
   @com.google.android.gms.common.annotation.KeepName *;
}

-keepnames class * implements android.os.Parcelable {
   public static final ** CREATOR;
}
```

### This App Bundle contains native code, and you've not uploaded debug symbols. We recommend you upload a symbol file to make your crashes and ANRs easier to analyze and debug.
