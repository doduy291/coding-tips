```js
// hooks/useMagicColor.tsx
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
const MagicBox = () => {
  const color = useMagicColor();
  return (
    <div className="container">
      <h3>useMagicColor Custorm Hook</h3>
      <br />
      <div className="magic-box" style={{ backgroundColor: `${color}` }}></div>
    </div>
  );
};
```

- Some CSS:

```js
.container {
  margin-left: 50px;
}

.magic-box {
  width: 200px;
  height: 200px;
  border-radius: 8px;
  transition: all 0.35s ease-in-out;
}
```
