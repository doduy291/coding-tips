```js
/* Typescript */
const copyToClipboard = (text: string) => {
  try {
    navigator.clipboard.writeText(text);
    return true;
  } catch (error) {
    return false;
  }
};

copyToClipboard("copy-test");
```
