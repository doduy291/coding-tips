import { MasonryFlashList } from "@shopify/flash-list";
import React from "react";
import { StyleProp, StyleSheet, View, ViewStyle } from "react-native";

interface Props {
  children: React.ReactNode;
  numColumns?: number;
  estimatedItemSize?: number;
  ItemSeparatorComponent?: React.ComponentType;
  style?: StyleProp<ViewStyle>;
}

const MasonryLayout: React.FC<Props> = ({
  children,
  numColumns = 1,
  estimatedItemSize = 20,
  ItemSeparatorComponent,
  style,
}) => {
  return (
    <View style={[styles.layoutContainer, style]}>
      <MasonryFlashList
        data={React.Children.toArray(children)}
        numColumns={numColumns}
        renderItem={RenderItem}
        estimatedItemSize={estimatedItemSize}
        keyExtractor={(_, index) => `${index}`}
        showsVerticalScrollIndicator={false}
        ItemSeparatorComponent={ItemSeparatorComponent}
      />
    </View>
  );
};

const RenderItem = ({ item }) => {
  return <View style={styles.itemContainer}>{item}</View>;
};

const styles = StyleSheet.create({
  layoutContainer: {
    width: "100%",
    height: "100%",
  },
  itemContainer: {
    flex: 1,
    alignItems: "center",
    justifyContent: "center",
  },
});
export default MasonryLayout;
