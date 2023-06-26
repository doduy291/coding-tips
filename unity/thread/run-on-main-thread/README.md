**Use this to run Methods on the Main Thread from another thread**
<br>
<br>

**Usage:**

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
