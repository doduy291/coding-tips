Ways to handle dynamic keys in objects with ES6:

- First way:

```js
let example = {
  name: "Cake",
};
let newKey = "logo";

example[newKey] = "🍰";

console.log(example); // Output { name: "Cake", logo: "🍰" }
```

- Second way:

```js
let newKey = "logo"
let example = {
    name: "Cake",
    [newKey] =  "🍰"
}

console.log(example); // Output { name: "Cake", logo: "🍰" }
```

Reference: https://www.samanthaming.com/tidbits/37-dynamic-property-name-with-es6/
