```js
// hooks/useEffectOnce.tsx
const useEffectOnce = (callback: () => void, when: boolean) => {
  const hasRunOnce = React.useRef(false);
  React.useEffect(() => {
    if (when && !hasRunOnce.current) {
      callback();
      hasRunOnce.current = true;
    }
  }, [when]);
};
```

_Usage_:

```js
// example.component.tsx
const Example = () => {
  const [clicked, setClicked] = React.useState(false);
  useEffectOnce(() => {
    console.log("mounted");
  }, clicked);
  return <button onClick={() => setClicked(true)}>Click me</button>;
};
```
