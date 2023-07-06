**Use this to run Methods on the Main Thread from another thread**

# Caution:

In Unity 2023 version, you would rather use this: <br>
https://docs.unity3d.com/2023.1/Documentation/ScriptReference/Awaitable.MainThreadAsync.html

# Some usages

<br>
<br>

**Usage:**
Add script to GameObject

1. Use `Dispatcher.cs`

```js
private void UpdateAmount(uint amount)
{
    Dispatcher.RunOnMainThread(() =>
    {
        gameObject.SetActive(true);
        amountText.text = $"{amount}";
        cvGroup.alpha = 1f;
    });
}
```

Source: I got it from here https://forum.unity.com/threads/admob-method-onadrewarded-simple-stupid-but-difficult-question.642151/

2. Use `UnityMainThreadDispatcher.cs`

```js
public IEnumerator ThisWillBeExecutedOnTheMainThread() {
	Debug.Log ("This is executed from the main thread");
	yield return null;
}
public void ExampleMainThreadCall() {
	UnityMainThreadDispatcher.Instance().Enqueue(ThisWillBeExecutedOnTheMainThread());
}

# OR
UnityMainThreadDispatcher.Instance().Enqueue(() => Debug.Log ("This is executed from the main thread"))
```

Source: https://github.com/PimDeWitte/UnityMainThreadDispatcher
