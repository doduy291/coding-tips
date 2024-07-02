import { Image, ImageSourcePropType } from "react-native";

const calcAspectRatioImageHeight = async (
  imageSource: string,
  imageWidthSize: number,
) => {
  const { width, height } = await getImageSize(imageSource);
  const aspectRatio = height / width;
  return imageWidthSize * aspectRatio;
};

// Only local image
const calcAspectRatioImageHeightLocal = (
  imageSource: ImageSourcePropType,
  imageWidthSize: number,
) => {
  const localImage = Image.resolveAssetSource(imageSource);
  const aspectRatio = localImage.height / localImage.width;
  return imageWidthSize * aspectRatio;
};

const getImageSize = async (uri: string) => {
  return new Promise<{ width: number; height: number }>((resolve, reject) => {
    Image.getSize(
      uri,
      (width, height) => {
        resolve({ width, height });
      },
      (error) => {
        console.error(`Failed to load image: ${uri}`, error);
        reject(error);
      },
    );
  });
};

export { calcAspectRatioImageHeight, calcAspectRatioImageHeightLocal };
