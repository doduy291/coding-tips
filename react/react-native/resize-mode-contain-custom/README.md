Here's how you can manually calculate the resizing logic for the `resizeMode: 'contain'` behavior:
<br>

**Steps to Calculate the Resized Dimensions:**

- Get the aspect ratios of both the image and the container.
- Compare the aspect ratios to determine whether to scale based on the width or height of the container.
- Adjust the width or height of the image accordingly while keeping the aspect ratio the same.

**Formula:**

- If the container's aspect ratio is wider than the image's aspect ratio, the image will fit based on the container's height.
- If the container's aspect ratio is narrower than the image's aspect ratio, the image will fit based on the container's width.

```js
import { View, Image } from "react-native";

const url = "https://i.imgur.com/your-img.jpg";
const screenWidth = Dimensions.get("screen").width;
const IMG_MAX_HEIGHT = 415;

const getImageDimensions = (uri, setState) => {
  if (uri.includes("http") || uri.includes("base64")) {
    Image.getSize(
      uri,
      (width, height) => {
        setState({ width, height, aspectRatio: width / height });
      },
      (error) => {
        console.error("Failed to get image dimensions", error);
      },
    );
  }
};

const ResizeModeContainCustom = () => {
  const [imgDimensions, setImgDimensions] = useState({
    width: 0,
    height: 0,
    aspectRatio: 1,
  });

  useEffect(() => {
    getImageDimensions(url, setImgDimensions);
  }, []);

  const ViewSize = () => {
    const scaledSize = {
      width: screenWidth,
      height: IMG_MAX_HEIGHT,
    };
    const ratio = imgDimensions.aspectRatio;
    const containerRatio = screenWidth / IMG_MAX_HEIGHT;

    if (containerRatio > ratio) {
      scaledSize.width = IMG_MAX_HEIGHT * ratio;
      scaledSize.height = IMG_MAX_HEIGHT;
    } else {
      scaledSize.width = screenWidth;
      scaledSize.height = screenWidth / ratio;
    }
    return { ...scaledSize };
  };
  return (
    <View>
      <Image style={[ViewSize()]} />
    </View>
  );
};
```
