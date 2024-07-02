import { useState, useEffect } from "react";
import { Dimensions } from "react-native";
import { calcAspectRatioImageHeight } from "./calcAspectRatioImage";

interface Props {
  data;
  totalGap: number;
  totalColumn: number;
}

interface Size {
  width?: number;
  height?: number;
}

interface ImageSize {
  [key: string]: Size;
}

const useMasonryImagesURI = (props: Props) => {
  const [imageSize, setImageSize] = useState<ImageSize>({});
  const { width } = Dimensions.get("screen");
  const imageWidth = (width - props.totalGap) / props.totalColumn; // Assuming 2 columns with ? gap (margin, padding)

  useEffect(() => {
    const GetSizeImages = async () => {
      const sizes = {};
      for (const item of props.data) {
        const height = await calcAspectRatioImageHeight(
          item.imageSource,
          imageWidth,
        );
        sizes[item.id] = { width: imageWidth, height };
      }
      setImageSize(sizes);
    };
    GetSizeImages();
  }, [imageWidth, props.data]);

  return { imageSize };
};

export default useMasonryImagesURI;
