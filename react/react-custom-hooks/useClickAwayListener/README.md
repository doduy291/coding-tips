```js
// hooks/useClickAwayListener.tsx
const useClickAwayListener = (initialValue: boolean) => {
  const clickRef = useRef < HTMLDivElement > null;
  const [isVisible, setVisible] = useState(initialValue);

  const handleClickOutside = (event: any) => {
    if (clickRef.current && !clickRef.current.contains(event.target)) {
      setVisible(false);
    }
  };

  useEffect(() => {
    /*
        Read more useCapture in eventLisnter
        @link: https://developer.mozilla.org/en-US/docs/Web/API/EventTarget/addEventListener
    */
    document.addEventListener("click", handleClickOutside, true);
    return () => {
      document.removeEventListener("click", handleClickOutside, true);
    };
  }, []);

  return { clickRef, isVisible, setVisible };
};
```

_Usage_:

```js
// example.component.tsx
const ClickAwayListener = () => {
  const { clickRef, isVisible, setVisible } = useClickAwayListener(false);

  return (
    <div className="wrapper" ref={clickRef}>
      <button
        className="button"
        onClick={() => setVisible((currentState) => !currentState)}
      >
        Click Me
      </button>
      {isVisible && (
        <ul className="list">
          <li className="list-item">dropdown option 1</li>
          <li className="list-item">dropdown option 2</li>
          <li className="list-item">dropdown option 3</li>
          <li className="list-item">dropdown option 4</li>
        </ul>
      )}
    </div>
  );
};
```

- Some CSS:

```js
.wrapper {
  display: inline-flex;
  flex-direction: column;
}

.button {
  margin: 20px 0px 0px 0px;
  border: 1px solid #2185d0;
  padding: 10px;
  border-radius: 5px;
  cursor: pointer;
  font-weight: bold;
  background-color: white;
  width: 140px;
}

.list {
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.175);
  border: 1px solid #ccc;
  list-style-type: none;
  padding: 0;
  margin: 0;
  width: auto;
  display: inline-block;
}
.list-item {
  padding: 8px;
  cursor: pointer;
  background-color: white;
}
.list-item:hover,
.list-item:active {
  background-color: #f3f3f3;
}

```
