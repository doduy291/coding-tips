### Caution

This one only supports in static modal. Not suitable for dynamic modals <br>
If you want to use multiple modal in global. Let's refer [this](https://github.com/susonthapa/react-native-global-modal)

### Implemention

#### Method 1

Using **NativeEventEmitter**

```js
// components/GlobalModal
const EventEmitter = new NativeEventEmitter();

const SHOW_GLOBAL_MODAL = "show_global_modal";
const HIDE_GLOBAL_MODAL = "hide_global_modal";

export const showGlobalModal = () => {
  EventEmitter.emit(SHOW_GLOBAL_MODAL);
};

export const hideGlobalModal = () => {
  EventEmitter.emit(HIDE_GLOBAL_MODAL);
};

const GlobalModal = React.memo(() => {
  const [modalVisible, setModalVisible] = useState(false);

  useEffect(() => {
    const showSub = EventEmitter.addListener(SHOW_GLOBAL_MODAL, () => {
      setModalVisible(true);
    });
    const hideSub = EventEmitter.addListener(HIDE_GLOBAL_MODAL, () => {
      setModalVisible(false);
    });
    return () => {
      showSub.remove();
      hideSub.remove();
    };
  }, []);

  return (
    <Modal transparent visible={modalVisible} animationType="none">
      // Your modal content
      <TouchableOpacity onPress={hideGlobalModal}>
        <Text>Close here</Text>
      </TouchableOpacity>
    </Modal>
  );
});
```

```js
// Declare the component in the top-level component (App.tsx)
const App: React.FC = () => {
  return (
    <Provider store={store}>
      <I18nextProvider i18n={i18next}>
        <AppNavigator />
        <GlobalModal />
      </I18nextProvider>
    </Provider>
  );
};
```

```js
// Usage
import {showGlobalModal} from './components/GlobalModal'

/*
* Component code/logic in between
*/
return (
 <SomeComponent/>
 <TouchableOpacity
  onPress={() => {
    showGlobalModal();
  }}
 />
)
```

#### Method 2

Using **useImperativeHandle**

```js
// controllers/ModalController
import { MutableRefObject } from "react";

export type GlobalModalRef = {
  show: () => void,
  hide: () => void,
};

export default class ModalController {
  static modalRef: MutableRefObject<GlobalModalRef>;
  static setModalRef = (ref: any) => {
    this.modalRef = ref;
  };

  static showGlobalModal = () => {
    this.modalRef.current?.show();
  };

  static hideGlobalModal = () => {
    this.modalRef.current?.hide();
  };
}
```

```js
// components/GlobalModal
import ModalController, { GlobalModalRef } from '../controllers/ModalController';

const GlobalModal = React.memo(() => {
  const [modalVisible, setModalVisible] = useState(false);
  const modalRef = useRef<GlobalModalRef>();

  useLayoutEffect(() => {
    ModalController.setModalRef(modalRef);
  }, []);

  useImperativeHandle(
    modalRef,
    () => ({
      show: () => {
        setModalVisible(true);
      },
      hide: () => {
        setModalVisible(false);
      },
    }),
    []
  );

  return (
    <Modal transparent visible={modalVisible} animationType="none">
      // Your modal content
      <TouchableOpacity onPress={ModalController.hideGlobalModal}>
        <Text>Close here</Text>
      </TouchableOpacity>
    </Modal>
  );
});
```

```js
// Declare the component in the top-level component (App.tsx)
const App: React.FC = () => {
  return (
    <Provider store={store}>
      <I18nextProvider i18n={i18next}>
        <AppNavigator />
        <GlobalModal />
      </I18nextProvider>
    </Provider>
  );
};
```

```js
// Usage
import ModalController from '../controllers/ModalController';

/*
* Component code/logic in between
*/
return (
 <SomeComponent/>
 <TouchableOpacity
  onPress={() => {
    ModalController.showGlobalModal();
  }}
 />
)
```
