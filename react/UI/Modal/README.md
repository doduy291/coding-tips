### Setup

1. Modal component

```js
// components/Modal.tsx
import React from "react";
import ReactDOM from "react-dom";

interface ModalProps {
  children: React.ReactNode;
  onClose: () => void;
}

const Modal: React.FC<ModalProps> = ({ children, onClose }) => {
  const clickRef = React.useRef<HTMLDivElement>(null);

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const clickHandler = (event: any) => {
    if (clickRef.current && !clickRef.current.contains(event.target)) {
      onClose();
    }
  };

  React.useEffect(() => {
    document.addEventListener("click", clickHandler, true);
    return () => {
      document.removeEventListener("click", clickHandler, true);
    };
  }, []);

  return ReactDOM.createPortal(
    <div className="modal">
      <div className="modal__container" ref={clickRef}>
        {children}

        <div className="modal__closeWrapper" onClick={onClose}>
          <svg
            className="modal__closeIcon"
            xmlns="http://www.w3.org/2000/svg"
            width="24"
            height="24"
          >
            <path d="M6.414 5A1 1 0 1 0 5 6.414L10.586 12 5 17.586A1 1 0 1 0 6.414 19L12 13.414 17.586 19A1 1 0 1 0 19 17.586L13.414 12 19 6.414A1 1 0 1 0 17.586 5L12 10.586 6.414 5Z"></path>
          </svg>
        </div>
      </div>
      <div className="modal__overlay"></div>
    </div>,
    document.querySelector("body") as HTMLBodyElement,
  );
};
```

2. Styling (Optional)

```js
.modal {
  position: relative;
}

.modal__container {
  position: fixed;
  top: 50%;
  left: 50%;
  z-index: 100;
  padding: 2rem;
  background-color: #f7f7f7;
  border-radius: 6px;
  box-shadow: 0 0 10px rgb(0 0 0 / 20%), 0 1px 20px 5px rgb(0 0 0 / 4%);
  transform: translate(-50%, -50%);
}

.modal__overlay {
  position: fixed;
  top: 0;
  left: 0;
  z-index: 99;
  width: 100%;
  height: 100%;
  background-color: rgb(0 0 0 / 40%);
}

.modal__closeWrapper {
  position: absolute;
  top: 0;
  right: -2rem;
  z-index: 999;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0.438rem;
  cursor: pointer;
  background-color: #fff;
  border-radius: 50%;
  box-shadow: 0 0 1px 1px rgb(0 0 0 / 20%);
  transform: translate(-50%, -50%);
}

.modal__closeIcon {
  opacity: 0.5;
  fill: #333;
}

```

### Usage

```js
import React from "react";
import Modal from "./components/Modal";
import "./styles.css";

function App() {
  const [isVisible, setVisible] = React.useState(false);
  return (
    <div className="App">
      <button onClick={() => setVisible(true)}>Click Show</button>
      {isVisible && (
        <Modal onClose={() => setVisible(false)}>
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Laudantium
          sint animi accusantium soluta illo est vero, praesentium iure
          repellendus, magnam similique nemo eius neque rerum corrupti adipisci
          quibusdam sapiente numquam!
        </Modal>
      )}
    </div>
  );
}

export default App;
```
