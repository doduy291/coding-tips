```js
// hooks/useOrientation.tsx
import { useEffect, useState } from "react";
import { Dimensions } from "react-native";

const useOrientation = () => {
  const [screenInfo, setScreenInfo] = useState(Dimensions.get("screen"));

  useEffect(() => {
    const updateLayout = (result: any) => {
      setScreenInfo(result.screen);
    };

    Dimensions.addEventListener("change", updateLayout);
    return () => Dimensions.removeEventListener("change", updateLayout);
  });

  return { ...screenInfo, isPortait: screenInfo.height > screenInfo.width };
};

export default useOrientation;
```

- Usage

```js
const orientation = useOrientation();

// API
orientation.width <br />
orientation.height <br />
orientation.isPortait
```
