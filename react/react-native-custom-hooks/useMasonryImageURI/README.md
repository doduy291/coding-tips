### Introduction

This hook using Masonry layout from [@shopify/flash-list](https://shopify.github.io/flash-list/docs/guides/masonry/).

### Usage

```js
const AlbumListScreen: React.FC = () => {
  const { imageSize } = useMasonryImagesURI({
    data: exampleData,
    totalColumn: 2,
    totalGap: 20,
  });

  return (
    <View style={styles.container}>
      <MasonryLayout numColumns={2} ItemSeparatorComponent={ItemSeparator}>
        {exampleData.map((item, index) => {
          return (
            <ImageItemCard
              imageSource={item.imageSource}
              width={imageSize[item.id].width}
              height={imageSize[item.id].height}
              key={index}
            />
          );
        })}
      </MasonryLayout>
    </View>
  );
};


# Example Data
const exampleData = [
  {
    id: 1,
    imageSource: 'your-img-uri',
  },
  {
    id: 2,
    imageSource: 'your-img-url',
  },
  {
    id: 3,
    imageSource: 'your-img-url',
  },
  {
    id: 4,
    imageSource: 'your-img-url',
  },
```
