```js
const picker = (obj, keys) => {
  const result = {};
  for (const propertyName in obj) {
    if (keys.includes(propertyName)) {
      result[propertyName] = obj[propertyName];
    }
  }
  return result;
};
const obj = { name: "test", email: "test", address: "name" };
const keys = ["name", "email"];

console.log(picker(obj, keys));
// Output: { name: 'test', email: 'test' }
```
