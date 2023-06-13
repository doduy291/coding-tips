### Prerequisite

Setup Firebase project to enable Google Analytics: https://firebase.google.com/docs/analytics/unity/start

### How to view Debug Log Event

#### Step 1: Install Android Logcat Package

Install **Android Logcat** through **Unity Package Manager**

#### Step 2: Setup device to implement debug

1. Select **Window** > **Analysis** > **Android Logcat** or `Alt + 6` to open **Android Logcat** <br>
2. In tab Android Logcat, select **Tools** > **Open Terminal** and type the following commands

```js
# Only Android

$ adb shell setprop debug.firebase.analytics.app package-name (Edit > Project Settings > Player > Identification)
$ adb shell setprop log.tag.FA VERBOSE
$ adb shell setprop log.tag.FA-SVC VERBOSE
$ adb logcat -s "FA","FA-SVC"

*Turn off debug (Optional)
$ adb shell setprop debug.firebase.analytics.app .none
```

#### Step 3: Check Debug

1. Build your project and run it on any device
2. In Firebase console, open your project
3. Select Analytics from the menu, go to Debug View and then wait it response log event
