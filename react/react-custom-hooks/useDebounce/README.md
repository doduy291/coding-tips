```js
type Timeout = null | ReturnType<typeof setTimeout>;

const useDebounce = () => {
  const timeoutRef = useRef<Timeout>(null);

  useEffect(() => {
    return () => {
      if (timeoutRef.current) {
        clearTimeout(timeoutRef.current);
      }
    };
  }, []);

  const debounceFn = (cbFn: () => void, delay: number) => {
    // Clear timeout
    clearTimeout(timeoutRef.current as keyof Timeout);

    // New Timeout
    timeoutRef.current = setTimeout(() => {
      cbFn();
    }, delay);
  };

  return debounceFn;
};
```

_Usage_:

```js
// example.component.tsx
const Example = () => {
  const debounce = useDebounce();
  const anyCallback = () => {
    /* Do something*/
  };

  const exampleHandler = () => {
    debounce(anyCallback, 1000);
  };

  return <button onClick={exampleHandler}>Click me</button>;
};
```
